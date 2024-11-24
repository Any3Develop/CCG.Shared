using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.SystemTimers;
using CCG.Shared.Game.Events.Context.Timer;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Runtime
{
    public class RuntimeTimer : IRuntimeTimer
    {
        public TimerConfig Config { get; }
        public IRuntimeTimerModel RuntimeModel { get; private set; }
        public IEventPublisher EventPublisher { get; }

        private readonly IPlayersCollection playersCollection;
        private readonly ISystemTimers systemTimers;
        private IDisposables disposables;
        private string timerId;

        public RuntimeTimer(
            TimerConfig config,
            IPlayersCollection playersCollection,
            IEventPublisher eventPublisher,
            ISystemTimers systemTimers)
        {
            Config = config;
            EventPublisher = eventPublisher;
            this.systemTimers = systemTimers;
            this.playersCollection = playersCollection;
        }
        
        public void Start()
        {
            if (disposables != null)
                return;
            
            systemTimers.EventsSource.Subscribe<SystemTimerTickEvent>(OnTimerTick).AddTo(ref disposables);
            systemTimers.EventsSource.Subscribe<SystemTimerEndedEvent>(() => PassTurn()).AddTo(disposables);
        }

        public void End()
        {
            if (disposables == null)
                return;
            
            SetOwner(null, false);
            SwitchState(TimerState.Ended);
            disposables?.Dispose();
            systemTimers?.Remove(timerId);
            timerId = null;
        }

        public IRuntimeTimer Sync(IRuntimeTimerModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            Start(RuntimeModel.TimeLeftMs);
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

        public void SwitchState(TimerState value, bool notify = true)
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

            
            var prevOwnerId = RuntimeModel.OwnerId;
            var nextPlayer = prevOwnerId == null
                ? playersCollection.First(x => x.RuntimeModel.IsFirst)
                : playersCollection.GetOpposite(prevOwnerId);

            var nextOwnerId = nextPlayer.RuntimeModel.OwnerId;
            if (RuntimeModel.Round % playersCollection.Count == 0)
                RuntimeModel.Round++;

            RuntimeModel.Turn++;
            RemoveEnding(false);
            SetOwner(nextOwnerId, false);
            Start(GetTimeByState());
            
            if (notify)
                EventPublisher.Publish(new TimerTurnChangedEvent(nextOwnerId, prevOwnerId));
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
            if (IsNotInitialized() || HasAny(TimerState.NotStarted))
                return;
            
            systemTimers.Remove(timerId);
            RuntimeModel.TimeLeftMs = durationMs;
            if (durationMs > 0)
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
            return RuntimeModel?.State is null || HasAny(TimerState.Ended);
        }

        private bool HasAny(TimerState flags)
        {
            return RuntimeModel != null && (RuntimeModel.State & flags) != 0;
        }
    }
}