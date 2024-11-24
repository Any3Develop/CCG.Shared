using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Objects;
using CCG.Shared.Game.Runtime.Args;

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

        public void Init(
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

        public void Sync(IRuntimeObjectModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            StatsCollection.LinkModelsList(RuntimeModel.Stats);
            EffectsCollection.LinkModelsList(RuntimeModel.Applied);
        }

        public bool ReceiveHit(HitArgs hit)
        {
            if (!hit.Attacker.IsAlive && !hit.Type.HasFlag(DamageType.CounterAttack))
                return false;
            
            if (!IsAlive || hit.Target != this)
                return false;
            
            return OnReceiveDamage(ref hit) | TryCounterAttack(ref hit);
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

        public virtual void Spawn(bool notify = true)
        {
            SetState(ObjectState.Table, notify:notify);
            
            if (notify)
                EventPublisher.Publish(new AfterObjectSpawnedEvent(this));
        }
        
        protected virtual bool OnReceiveDamage(ref HitArgs hit)
        {
            var result = false;
            var damage = hit.Damage;
            if (StatsCollection.TryGet(StatType.Armor, out var armorStat) && armorStat.Current > 0)
            {
                armorStat.Subtract(1);
                if (armorStat.Current <= 0)
                    StatsCollection.Remove(armorStat);

                damage = 0;
                result = true;
            }
            
            if (damage > 0 && StatsCollection.TryGet(StatType.Hp, out var hpStat))
            {
                hpStat.Subtract(damage);
                result = true;
            }

            if (result)
                EventPublisher.Publish(new AfterObjectHitReceivedEvent(hit));
            
            return result;
        }

        protected virtual bool TryCounterAttack(ref HitArgs hit)
        {
            if (hit.Type.HasFlag(DamageType.Direct | DamageType.None) 
                && StatsCollection.TryGet(StatType.Attack, out var attackStat) 
                && attackStat.Current > 0)
                return hit.Attacker.ReceiveHit(new HitArgs(this, hit.Attacker, attackStat.Current, DamageType.CounterAttack));

            return false;
        }
        
        protected abstract bool IsObjectAlive();

        #region IRuntimeBase
        
        IRuntimeBaseModel IRuntimeBase.RuntimeModel => RuntimeModel;
        
        IConfig IRuntimeBase.Config => Config;

        #endregion
    }
}