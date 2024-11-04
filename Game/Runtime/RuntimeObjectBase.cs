using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Objects;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Runtime
{
    public abstract class RuntimeObjectBase : IRuntimeObject
    {
        public ObjectConfig Config { get; private set; }
        public IRuntimeObjectModel RuntimeModel { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEffectsCollection EffectsCollection { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }
        public IEventsSource EventsSource { get; private set; }
        
        protected bool Initialized { get; private set; }

        public IRuntimeObject Init(
            ObjectConfig config,
            IStatsCollection statsCollection,
            IEffectsCollection effectCollection,
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Config = config;
            StatsCollection = statsCollection;
            EffectsCollection = effectCollection;
            EventPublisher = eventPublisher;
            EventsSource = eventsSource;
            Initialized = true;
            return this;
        }
        
        public virtual void Dispose()
        {
            if (!Initialized)
                return;

            Initialized = false;
            EventsSource?.Dispose();
            StatsCollection?.Clear();
            EffectsCollection?.Clear();
            Config = null;
            RuntimeModel = null;
            EventsSource = null;
            StatsCollection = null;
            EffectsCollection = null;
        }

        public IRuntimeObject Sync(IRuntimeObjectModel runtimeModel, bool notify = true)
        {
            if (!Initialized)
                return this;
            
            EventPublisher.Publish<BeforeObjectChangeEvent>(notify, this);
            RuntimeModel = runtimeModel;
            EventPublisher.Publish<AfterObjectChangedEvent>(notify, this);
            return this;
        }

        public void SetState(ObjectState value, bool notify = true)
        {
            if (!Initialized)
                return;
            
            EventPublisher.Publish<BeforeObjectStateChangeEvent>(notify, this);
            RuntimeModel.PreviousState = RuntimeModel.State;
            RuntimeModel.State = value;
            EventPublisher.Publish<AfterObjectStateChangedEvent>(notify, this);
        }

        #region IRuntimeObjectBase

        IRuntimeModelBase IRuntimeObjectBase.RuntimeModel => RuntimeModel;
        
        IConfig IRuntimeObjectBase.Config => Config;

        #endregion
    }
}