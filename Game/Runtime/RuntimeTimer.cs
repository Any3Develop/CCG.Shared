using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime
{
    public class RuntimeTimer : IRuntimeTimer
    {
        public TimerConfig Config { get; private set; }
        public IRuntimeTimerModel RuntimeModel { get; private set; }

        public IEventsSource EventsSource { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }

        public void Init(
            TimerConfig config,
            IRuntimeTimerModel runtimeModel,
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Config = config;
            RuntimeModel = runtimeModel;
            EventPublisher = eventPublisher;
            EventsSource = eventsSource;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IRuntimeTimer Sync(IRuntimeTimerModel runtimeModel, bool notify = true)
        {
            RuntimeModel = runtimeModel;
            // TODO notify
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

        #region IRuntimeObjectBase

        IRuntimeModelBase IRuntimeObjectBase.RuntimeModel => RuntimeModel;

        IConfig IRuntimeObjectBase.Config => Config;

        #endregion
    }
}