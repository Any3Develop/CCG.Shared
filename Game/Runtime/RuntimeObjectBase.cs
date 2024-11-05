using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Objects;
using CCG.Shared.Game.Utils;
using CCG.Shared.Game.Utils.Disposables;

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

        protected readonly IDisposables Disposables = DisposableExtensions.CreateDisposables();

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
            
            Disposables.Add(EventsSource);
            Disposables.Add(StatsCollection);
            Disposables.Add(EffectsCollection);
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

        public void AddStat(IRuntimeStat stat, bool notify = true)
        {
            if (StatsCollection.Contains(stat.RuntimeModel.Id))
                throw new InvalidOperationException($"Can't add stat twice : {stat.RuntimeModel.ReflectionFormat()}");

            RuntimeModel.Stats.Add(stat.RuntimeModel);
            StatsCollection.Add(stat, notify);
        }

        public void RemoveStat(IRuntimeStat stat, bool notify = true)
        {
            if (!StatsCollection.Contains(stat.RuntimeModel.Id))
                return;

            RuntimeModel.Stats.RemoveAll(x => x.Id == stat.RuntimeModel.Id);
            StatsCollection.Remove(stat, notify);
            stat.Dispose();
        }

        public void AddEffect(IRuntimeEffect effect, bool notify = true)
        {
            if (EffectsCollection.Contains(effect.RuntimeModel.Id))
                throw new InvalidOperationException($"Can't add effect twice : {effect.RuntimeModel.ReflectionFormat()}");

            RuntimeModel.Applied.Add(effect.RuntimeModel);
            EffectsCollection.Add(effect, notify);
        }

        public void RemoveEffect(IRuntimeEffect effect, bool notify = true)
        {
            if (!EffectsCollection.Contains(effect.RuntimeModel.Id))
                return;

            RuntimeModel.Applied.RemoveAll(x => x.Id == effect.RuntimeModel.Id);
            EffectsCollection.Remove(effect, notify);
            effect.Dispose();
        }

        #region IRuntimeObjectBase

        IRuntimeModelBase IRuntimeObjectBase.RuntimeModel => RuntimeModel;
        
        IConfig IRuntimeObjectBase.Config => Config;

        #endregion
    }
}