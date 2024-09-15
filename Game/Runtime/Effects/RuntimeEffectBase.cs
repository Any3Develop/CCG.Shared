using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Events.Context.Effects;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Runtime.Effects
{
    public abstract class RuntimeEffectBase : IRuntimeEffect
    {
        public EffectConfig Config { get; private set; }
        public IRuntimeEffectModel RuntimeModel { get; private set; }
        public IEventsSource EventsSource { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }

        protected bool Initialized { get; private set; }

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
            Initialized = true;
            return this;
        }

        public void Dispose()
        {
            if (!Initialized)
                return;

            Initialized = false;
            Config = null;
            RuntimeModel = null;
            EventsSource = null;
        }

        public IRuntimeEffect Sync(IRuntimeEffectModel runtimeModel, bool notify = true)
        {
            if (!Initialized)
                return this;
            
            EventPublisher.Publish<BeforeEffectChangeEvent>(notify, this);
            RuntimeModel = runtimeModel;
            EventPublisher.Publish<AfterEffectChangedEvent>(notify, this);
            return this;
        }

        public bool IsExecuteAllowed() => true;

        public void Execute()
        {
            if (!Initialized)
                return;
            
            EventPublisher.Publish<BeforeEffectExecuteEvent>(Initialized, this);
            OnExecute();
            EventPublisher.Publish<AfterEffectExecutedEvent>(Initialized, this);
        }

        public void Expire()
        {
            if (!Initialized)
                return;
            
            EventPublisher.Publish<BeforeEffectExpireEvent>(Initialized, this);
            OnExpire();
            EventPublisher.Publish<AfterEffectExpiredEvent>(Initialized, this);
        }

        #region Callbacks
        protected virtual void OnExecute() {}
        protected virtual void OnExpire() {}
        #endregion

        #region IRuntimeObjectBase

        IRuntimeModelBase IRuntimeObjectBase.RuntimeModel => RuntimeModel;

        IConfig IRuntimeObjectBase.Config => Config;

        #endregion
    }
}