using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.SystemTimers;
using CCG.Shared.Game.Events.Context.Timer;
using CCG.Shared.Game.Runtime.Models;
using CCG.Shared.Game.Utils.Disposables;

namespace CCG.Shared.Game.Runtime
{
    public class RuntimeTimer : IRuntimeTimer
    {
        public TimerConfig Config { get; private set; }
        public IRuntimeTimerModel RuntimeModel { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }

        private IPlayersCollection playersCollection;
        private ISystemTimers systemTimers;
        private IDisposables disposables;
        private string timerId;

        public RuntimeTimer(
            TimerConfig config,
            IRuntimeTimerModel runtimeModel,
            IPlayersCollection playersCollection,
            IEventPublisher eventPublisher,
            ISystemTimers systemTimers)
        {
            Config = config;
            EventPublisher = eventPublisher;
            this.systemTimers = systemTimers;
            this.playersCollection = playersCollection;

            this.systemTimers.EventsSource.Subscribe<SystemTimerTickEvent>(OnTimerTick).AddTo(ref disposables);
            this.systemTimers.EventsSource.Subscribe<SystemTimerEndedEvent>(() => PassTurn()).AddTo(disposables);
            Sync(runtimeModel);
        }

        public void Dispose()
        {
            if (disposables == null)
                return;
            
            disposables?.Dispose();
            systemTimers?.Remove(timerId);
            Config = null;
            RuntimeModel = null;
            EventPublisher = null;
            playersCollection = null;
            systemTimers = null;
            disposables = null;
        }

        public IRuntimeTimer Sync(IRuntimeTimerModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            Start(runtimeModel.TimeLeftMs);
            return this;
        }

        public void SetState(TimerState value, bool notify = true)
        {
            if (IsNotInitialized())
                return;
            
            RuntimeModel.State |= value;
            OnChanged(notify);
        }

        public void RemoveState(TimerState value, bool notify = true)
        {
            if (IsNotInitialized() || !HasAny(value))
                return;
            
            RuntimeModel.State &= ~value;
            OnChanged(notify);
        }

        public void SwitchState(TimerState value, bool notify)
        {
            if (IsNotInitialized())
                return;
            
            RuntimeModel.State = value;
            OnChanged(notify);
        }

        public void SetOwner(string value, bool notify = true)
        {
            if (IsNotInitialized())
                return;
            
            RuntimeModel.OwnerId = value;
            OnChanged(notify);
        }

        public void Pause(bool value, bool notify = true)
        {
            if (IsNotInitialized() || !systemTimers.Pause(timerId, value))
                return;
            
            if (value)
                SetState(TimerState.Paused, false);
            else 
                RemoveState(TimerState.Paused, false);
            
            OnChanged(notify);
        }
        
        public void PassTurn(bool notify = true)
        {
            if (IsNotInitialized() 
                || HasAny(TimerState.NotStarted)
                || TryStartEnding(notify))
                return;
            
            var nextPlayer = RuntimeModel.OwnerId == null
                ? playersCollection.First(x => x.RuntimeModel.IsFirst)
                : playersCollection.GetOpposite(RuntimeModel.OwnerId);
            
            RuntimeModel.Turn++;
            if (RuntimeModel.Round % playersCollection.Count == 0)
                RuntimeModel.Round++;

            RemoveEnding(false);
            SetOwner(nextPlayer.RuntimeModel.OwnerId, false);
            Start(GetTimeByState());
            
            if (notify)
                EventPublisher.Publish(new TimerTurnChangedEvent(this));
        }
        
        public void SetActionTime(int durationMs, bool inParallel = false)
        {
            if (IsNotInitialized())
                return;
            
            RuntimeModel.Actions.Add(new ActionTimestamp(RuntimeModel.TimeLeftMs, durationMs, inParallel));
        }

        public void RemoveEnding(bool notify = true)
        {
            if (IsNotInitialized())
                return;
            
            RuntimeModel.Actions.Clear();
            RemoveState(TimerState.Ending, false);
        }

        private bool TryStartEnding(bool notify)
        {
            // if there a pause, it should block execution of ending function.
            if (HasAny(TimerState.Paused))
                return false;
            
            // block switch until all actions ended
            if (HasAny(TimerState.Ending) && RuntimeModel.TimeLeftMs > 1)
                return true;

            if (RuntimeModel.Actions.Count == 0)
                return false;

            var endTime = RuntimeModel.Actions.Aggregate(int.MaxValue, (endTime, action) =>
            {
                if (action.InParallel) // exclude a parallel action from the time stack if it hasn't exceeded the endTime.
                    return Math.Min(action.Timestamp - action.Duration, endTime);
                
                // normal queue time stack, it will choose the latest endTime.
                return Math.Min(endTime, action.Timestamp) - action.Duration;
            });
            RuntimeModel.Actions.Clear();

            if (endTime >= RuntimeModel.TimeLeftMs) 
                return false;

            var unMetTime = RuntimeModel.TimeLeftMs -= endTime;
            SetState(TimerState.Ending, false);
            Start(unMetTime);
            OnChanged(notify);
            return true;
        }
        
        private void OnChanged(bool notify)
        {
            if (IsNotInitialized())
                return;
            
            if (notify)
                EventPublisher.Publish(new TimerChangedEvent(this));
        }

        private void Start(int durationMs)
        {
            if (IsNotInitialized())
                return;
            
            systemTimers.Remove(timerId);
            RuntimeModel.TimeLeftMs = durationMs;
            timerId = systemTimers.Start(durationMs, 0, 1000);
            systemTimers.Pause(timerId, HasAny(TimerState.Paused));
        }
        
        private int GetTimeByState()
        {
            if (IsNotInitialized())
                return 0;
            
            if (HasAny(TimerState.GamePlay | TimerState.Paused | TimerState.Ending))
                return Config.RoundMs;
            
            if (HasAny(TimerState.NotStarted | TimerState.Ended))
                return 0;

            if (HasAny(TimerState.Mulligan))
                return Config.MulliganMs;

            throw new NotImplementedException($"Unknown {nameof(TimerState)} : {RuntimeModel.State}.");
        }

        private void OnTimerTick(SystemTimerTickEvent eventData)
        {
            if (timerId != eventData.TimerId || IsNotInitialized())
                return;
            
            RuntimeModel.TimeLeftMs = eventData.CurrentMs;
        }

        private bool IsNotInitialized()
        {
            if (RuntimeModel?.State is not (null or TimerState.Ended)) 
                return false;
            
            Dispose();
            return true;
        }

        private bool HasAny(TimerState flags)
        {
            return RuntimeModel != null && (RuntimeModel.State & flags) != 0;
        }

        private bool HasAll(TimerState flags)
        {
            return RuntimeModel != null && (RuntimeModel.State & flags) == flags;
        }
    }
}