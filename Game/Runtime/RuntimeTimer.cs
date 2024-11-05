using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime
{
    public class RuntimeTimer : IRuntimeTimer
    {
        public TimerConfig Config { get; }
        public IRuntimeTimerModel RuntimeModel { get; private set; }
        public IEventsSource EventsSource { get; }
        public IEventPublisher EventPublisher { get; }

        public RuntimeTimer(
            TimerConfig config,
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Config = config;
            EventPublisher = eventPublisher;
            EventsSource = eventsSource;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IRuntimeTimer Sync(IRuntimeTimerModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            return this;
        }

        public void SetState(TimerState value, bool notify = true)
        {
            // TODO notify
            throw new NotImplementedException();
        }

        public void SetTurnOwner(string value, bool notify = true)
        {
            // TODO notify
            throw new NotImplementedException();
        }

        public void PassTurn(bool notify = true)
        {
            // TODO notify
            throw new NotImplementedException();
        }
    }
}