using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
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
            
            RuntimeModel.State = value;
            OnChanged(notify);
        }

        public void SetTurnOwner(string value, bool notify = true)
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

            RuntimeModel.Paused = value;
            OnChanged(notify);
        }
        
        public void PassTurn(bool notify = true)
        {
            if (IsNotInitialized() || RuntimeModel.State is TimerState.NotStarted)
                return;
            
            if (ShouldWaitTurn(notify))
                return;
            
            var nextPlayer = RuntimeModel.OwnerId == null
                ? playersCollection.First(x => x.RuntimeModel.IsFirst)
                : playersCollection.GetOpposite(RuntimeModel.OwnerId);
            
            RuntimeModel.Turn++;
            if (RuntimeModel.Round % playersCollection.Count == 0)
                RuntimeModel.Round++;
            
            if (RuntimeModel.State is TimerState.GameTurnEnding)
                SetState(TimerState.Game, false);
            
            SetTurnOwner(nextPlayer.RuntimeModel.OwnerId, false);
            Start(GetTimeByState());
            
            if (notify)
                EventPublisher.Publish(new TimerTurnChangedEvent(this));
        }
        
        public void RegisterAction(int durationMs, bool inParallel = false)
        {
            if (IsNotInitialized())
                return;
            
            RuntimeModel.Actions.Add(new ActionTimestamp(RuntimeModel.TimeLeftMs, durationMs, inParallel));
        }

        private bool ShouldWaitTurn(bool notify)
        {
            if (IsNotInitialized())
                return true;
            
            // block custom switch until all actions ended
            if (RuntimeModel.State is TimerState.GameTurnEnding && RuntimeModel.TimeLeftMs > 1)
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
            SetState(TimerState.GameTurnEnding, false);
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

        private void Start(int duration)
        {
            if (IsNotInitialized())
                return;
            
            systemTimers.Remove(timerId);
            timerId = systemTimers.Start(duration, 0, 1000);
        }
        
        private int GetTimeByState()
        {
            if (IsNotInitialized())
                return 0;
            
            return RuntimeModel.State switch
            {
                TimerState.NotStarted or TimerState.End => 0,
                TimerState.Mulligan => Config.MulliganMs,
                TimerState.Game => Config.RoundMs,
                _ => throw new NotImplementedException($"Unknown {nameof(TimerState)} : {RuntimeModel.State}.")
            };
        }

        private void OnTimerTick(SystemTimerTickEvent eventData)
        {
            if (timerId != eventData.TimerId || IsNotInitialized())
                return;
            
            RuntimeModel.TimeLeftMs = eventData.CurrentMs;
        }

        private bool IsNotInitialized()
        {
            if (RuntimeModel?.State is not (null or TimerState.End)) 
                return false;
            
            Dispose();
            return true;
        }
    }
}