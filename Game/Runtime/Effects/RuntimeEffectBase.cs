using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Events.Context.Effects;

namespace CCG.Shared.Game.Runtime.Effects
{
    public abstract class RuntimeEffectBase : IRuntimeEffect
    {
        public EffectConfig Config { get; private set; }
        public IRuntimeEffectModel RuntimeModel { get; private set; }
        public IEventsSource EventsSource { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }

        public IRuntimeEffect Init(
            EffectConfig config,
            IRuntimeEffectModel runtimeModel,
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Config = config;
            RuntimeModel = runtimeModel;
            EventPublisher = eventPublisher;
            EventsSource = eventsSource;
            OnCreated();
            return this;
        }

        public void Dispose()
        {
            Config = null;
            RuntimeModel = null;
            EventsSource = null;
        }

        public IRuntimeEffect Sync(IRuntimeEffectModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            return this;
        }

        public bool IsExecuteAllowed() => true;

        public void Execute()
        {
            EventPublisher.Publish(new BeforeEffectExecuteEvent(this));
            OnExecute();
            EventPublisher.Publish(new AfterEffectExecutedEvent(this));
        }

        public void Expire()
        {
            EventPublisher.Publish(new BeforeEffectExpireEvent(this));
            OnExpire();
            EventPublisher.Publish(new AfterEffectExpiredEvent(this));
        }

        #region Callbacks
        protected virtual void OnCreated() {}
        protected virtual void OnExecute() {}
        protected virtual void OnExpire() {}
        #endregion

        #region IRuntimeObjectBase

        IRuntimeModelBase IRuntimeObjectBase.RuntimeModel => RuntimeModel;

        IConfig IRuntimeObjectBase.Config => Config;

        #endregion
    }
}