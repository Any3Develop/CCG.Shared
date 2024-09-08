using CCG_Shared.Abstractions.Game.Collections;
using CCG_Shared.Abstractions.Game.Context.EventSource;
using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Abstractions.Game.Runtime.Objects;
using CCG_Shared.Game.Data;
using CCG_Shared.Game.Data.Enums;
using CCG_Shared.Game.Events.Context.Objects;
using CCG_Shared.Game.Utils;

namespace CCG_Shared.Game.Runtime.Objects
{
    public abstract class RuntimeObject : IRuntimeObject
    {
        public ObjectData Data { get; private set; }
        public IRuntimeObjectData RuntimeData { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEffectsCollection EffectsCollection { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }
        public IEventsSource EventsSource { get; private set; }
        
        protected bool Initialized { get; private set; }

        public IRuntimeObject Init(
            ObjectData data,
            IRuntimeObjectData runtimeData,
            IStatsCollection statsCollection,
            IEffectsCollection effectCollection,
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Data = data;
            RuntimeData = runtimeData;
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
            Data = null;
            RuntimeData = null;
            EventsSource = null;
            StatsCollection = null;
            EffectsCollection = null;
        }

        public IRuntimeObject Sync(IRuntimeObjectData runtimeData, bool notify = true)
        {
            if (!Initialized)
                return this;
            
            EventPublisher.Publish<BeforeObjectChangeEvent>(notify, this);
            RuntimeData = runtimeData;
            EventPublisher.Publish<AfterObjectChangedEvent>(notify, this);
            return this;
        }

        public void SetState(ObjectState value, bool notify = true)
        {
            if (!Initialized)
                return;
            
            EventPublisher.Publish<BeforeObjectStateChangeEvent>(notify, this);
            RuntimeData.PreviousState = RuntimeData.State;
            RuntimeData.State = value;
            EventPublisher.Publish<AfterObjectStateChangedEvent>(notify, this);
        }

        #region IRuntimeObjectBase

        IRuntimeDataBase IRuntimeObjectBase.RuntimeData => RuntimeData;

        #endregion
    }
}