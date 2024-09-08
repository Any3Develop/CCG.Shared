﻿using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Runtime.Data;
using Shared.Abstractions.Game.Runtime.Effects;
using Shared.Abstractions.Game.Runtime.Objects;
using Shared.Game.Data;
using Shared.Game.Events.Context.Effects;
using Shared.Game.Utils;

namespace Shared.Game.Runtime.Effects
{
    public abstract class RuntimeEffect : IRuntimeEffect
    {
        public EffectData Data { get; private set; }
        public IRuntimeEffectData RuntimeData { get; private set; }
        public IEventsSource EventsSource { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }

        protected bool Initialized { get; private set; }

        public IRuntimeEffect Init(
            EffectData data,
            IRuntimeEffectData runtimeData,
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
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
            Data = null;
            RuntimeData = null;
            EventsSource = null;
        }

        public IRuntimeEffect Sync(IRuntimeEffectData runtimeData, bool notify = true)
        {
            if (!Initialized)
                return this;
            
            EventPublisher.Publish<BeforeEffectChangeEvent>(notify, this);
            RuntimeData = runtimeData;
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

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}