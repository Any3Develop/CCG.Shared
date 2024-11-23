using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Objects;

namespace CCG.Shared.Game.Runtime
{
    public abstract class RuntimeObjectBase : IRuntimeObject
    {
        public ObjectConfig Config { get; private set; }
        public IRuntimeObjectModel RuntimeModel { get; private set; }
        public bool IsAlive => IsObjectAlive();
        public IStatsCollection StatsCollection { get; private set; }
        public IEffectsCollection EffectsCollection { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected IDisposables Disposables;

        public IRuntimeObject Init(
            ObjectConfig config,
            IRuntimeObjectModel runtimeModel,
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

            EventsSource.AddTo(ref Disposables);
            StatsCollection.AddTo(Disposables);
            EffectsCollection.AddTo(Disposables);
            Sync(runtimeModel);
            return this;
        }
        
        public virtual void Dispose()
        {
            Disposables?.Dispose();
            Config = null;
            RuntimeModel = null;
            EventsSource = null;
            StatsCollection = null;
            EffectsCollection = null;
        }

        public IRuntimeObject Sync(IRuntimeObjectModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            StatsCollection.LinkModelsList(RuntimeModel.Stats);
            EffectsCollection.LinkModelsList(RuntimeModel.Applied);
            return this;
        }

        public void SetState(ObjectState value, ObjectState? previous = null, bool notify = true)
        {
            if (notify)
                EventPublisher.Publish(new BeforeObjectStateChangeEvent(this));
            
            RuntimeModel.PreviousState = previous ?? RuntimeModel.State;
            RuntimeModel.State = value;
            
            if (notify)
                EventPublisher.Publish(new AfterObjectStateChangedEvent(this));
        }
        
        #region IRuntimeObjectBase
        protected abstract bool IsObjectAlive();
        
        IRuntimeModelBase IRuntimeObjectBase.RuntimeModel => RuntimeModel;
        
        IConfig IRuntimeObjectBase.Config => Config;

        #endregion
    }
}