using System.Collections.Concurrent;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Game.Events.Context.SystemTimers;

namespace CCG.Shared.Game.Context.Providers
{
    public class SystemTimers : ISystemTimers
    {
        protected class TimerData
        {
            public string Id;
            public Timer Instance;
            
            public int FromMs;
            public int ToMs;
            public int TickPeriodMs;
            public int TickValue;
            public bool IsCountingUp;
            public bool Paused;
            public bool TickNotify;
            
            public int CurrentMs
            {
                get => Interlocked.CompareExchange(ref currentMs, 0, 0);
                set => Interlocked.Exchange(ref currentMs, value);
            }
            private int currentMs;
        }

        public IEventsSource EventsSource { get; }
        public IEventPublisher EventPublisher { get; }

        protected readonly ConcurrentDictionary<string, TimerData> Actives;
        private readonly SynchronizationContext mainThreadSyncContext;

        public SystemTimers(IEventsSource eventsSource, IEventPublisher eventPublisher)
        {
            EventsSource = eventsSource;
            EventPublisher = eventPublisher;
            Actives = new ConcurrentDictionary<string, TimerData>();
            mainThreadSyncContext = SynchronizationContext.Current;
        }

        public int? Current(string timerId)
        {
            if (string.IsNullOrEmpty(timerId) || !Actives.TryGetValue(timerId, out var timerData))
                return null;

            return timerData.CurrentMs;
        }

        public string Start(int durationMs)
        {
            return Start(0, durationMs, 1000);
        }

        public string Start(int fromMs, int toMs, int tickPeriodMs, bool tickNotify = true)
        {
            var timerData = new TimerData
            {
                Id = Guid.NewGuid().ToString(),
                FromMs = fromMs,
                ToMs = toMs,
                TickPeriodMs = tickPeriodMs,
                TickValue = fromMs <= toMs ? 1 : -1,
                IsCountingUp = fromMs <= toMs,
                CurrentMs = fromMs,
                TickNotify = tickNotify
            };

            timerData.Instance = new Timer(_ => OnTick(timerData), null, 0, tickPeriodMs);
            Actives[timerData.Id] = timerData;

            Notify(new SystemTimerStartedEvent(timerData.Id));

            return timerData.Id;
        }

        public bool Remove(string timerId)
        {
            return !string.IsNullOrEmpty(timerId)
                   && Actives.TryGetValue(timerId, out var timerData)
                   && InternalRemove(timerData, true);
        }

        public bool Pause(string timerId, bool value)
        {
            if (string.IsNullOrEmpty(timerId) 
                || !Actives.TryGetValue(timerId, out var timerData) 
                || timerData?.Instance == null)
                return false;

            timerData.Paused = value;
            timerData.Instance.Change(0, timerData.TickPeriodMs);

            Notify(new SystemTimerPauseEvent(timerId, value));

            return true;
        }

        public bool Restart(string timerId)
        {
            if (string.IsNullOrEmpty(timerId) 
                || !Actives.TryGetValue(timerId, out var timerData) 
                || timerData?.Instance == null)
                return false;

            timerData.CurrentMs = timerData.FromMs;
            timerData.Instance.Change(0, timerData.TickPeriodMs);

            Notify(new SystemTimerRestartedEvent(timerId));

            return true;
        }
        
        public void Dispose()
        {
            var timerDatas = Actives.Values.ToArray();
            Actives.Clear();
            
            foreach (var timerData in timerDatas)
                Dispose(timerData);
        }
        
        protected bool InternalRemove(TimerData timerData, bool cancelled)
        {
            if (timerData?.Instance == null || !Actives.TryRemove(timerData.Id, out timerData))
                return false;

            Dispose(timerData);
            Notify(cancelled
                ? new SystemTimerCancelledEvent(timerData.Id)
                : new SystemTimerEndedEvent(timerData.Id));

            return true;
        }

        protected virtual void Dispose(TimerData timerData)
        {
            if (timerData?.Instance == null)
                return;
            
            timerData.Instance.Dispose();
            timerData.Instance = null;
        }

        protected void Notify(object eventData)
        {
            if (mainThreadSyncContext != null)
            {
                mainThreadSyncContext.Post(_ => EventPublisher.Publish(eventData), null);
                return;
            }

            EventPublisher.Publish(eventData);
        }
        
        private void OnTick(TimerData timerData)
        {
            if (timerData.Paused)
                return;

            if ((timerData.IsCountingUp && timerData.CurrentMs < timerData.ToMs) ||
                (!timerData.IsCountingUp && timerData.CurrentMs > timerData.ToMs))
            {
                timerData.CurrentMs += timerData.TickValue;

                if (timerData.TickNotify)
                    Notify(new SystemTimerTickEvent(timerData.Id, timerData.CurrentMs));

                return;
            }

            InternalRemove(timerData, false);
        }
    }
}