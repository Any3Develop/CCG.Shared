﻿using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventProcessors;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Events.Context.Cards;
using CCG.Shared.Game.Events.Context.Effects;
using CCG.Shared.Game.Events.Context.Objects;
using CCG.Shared.Game.Events.Context.Stats;
using CCG.Shared.Game.Events.Output;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Context.EventProcessors
{
    public class ObjectEventProcessor : IObjectEventProcessor
    {
        private readonly IGameQueueCollector queueCollector;

        public ObjectEventProcessor(IGameQueueCollector queueCollector)
        {
            this.queueCollector = queueCollector;
        }

        public void Subscribe(IRuntimeObjectBase runtimeObject)
        {
            OnSubscribe(runtimeObject);
        }

        protected virtual void OnSubscribe(IRuntimeObjectBase runtimeObject)
        {
            var eventSource = runtimeObject.EventsSource;

            #region Object

            eventSource.Subscribe<AfterObjectAddedEvent>(data =>
                queueCollector.Register(new AddedObject{RuntimeModel = data.RuntimeObject.RuntimeModel.Clone()}));

            eventSource.Subscribe<AfterObjectDeletedEvent>(data =>
                queueCollector.Register(new ObjectDeleted{RuntimeModel = data.RuntimeObject.RuntimeModel.Clone()}));
            
            eventSource.Subscribe<AfterObjectStateChangedEvent>(data =>
                queueCollector.Register(new ObjectStateChanged().Map(data.RuntimeObject.RuntimeModel)));
            
            eventSource.Subscribe<AfterCardPositionChangedEvent>(data =>
                queueCollector.Register(new CardPositionChanged().Map(data.RuntimeObject.RuntimeModel)));
            
            // TODO: execute hit effect
            // eventSource.Subscribe<AfterObjectHitEvent>(data =>
            //     queueCollector.Register(new HitObject{RuntimeData = data.RuntimeStat.RuntimeData.Clone()}));

            #endregion

            #region Effects

            eventSource.Subscribe<AfterEffectAddedEvent>(data =>
                queueCollector.Register(new EffectAdded{RuntimeModel = data.RuntimeEffect.RuntimeModel.Clone()})); 
            
            eventSource.Subscribe<AfterEffectDeletedEvent>(data =>
                queueCollector.Register(new EffectDeleted{RuntimeModel = data.RuntimeEffect.RuntimeModel.Clone()})); 
            
            eventSource.Subscribe<AfterEffectChangedEvent>(data =>
                queueCollector.Register(new EffectChanged{RuntimeModel = data.RuntimeEffect.RuntimeModel.Clone()})); 
            
            eventSource.Subscribe<BeforeEffectExecuteEvent>(data =>
                queueCollector.Register(new EffectStarted{RuntimeModel = data.RuntimeEffect.RuntimeModel.Clone()})); 
                        
            eventSource.Subscribe<AfterEffectExecutedEvent>(data =>
                queueCollector.Register(new EffectEnded{RuntimeModel = data.RuntimeEffect.RuntimeModel.Clone()}));  

            #endregion

            #region Stats

            eventSource.Subscribe<AfterStatAddedEvent>(data =>
                queueCollector.Register(new StatAdded{RuntimeModel = data.RuntimeStat.RuntimeModel.Clone()}));

            eventSource.Subscribe<AfterStatDeletedEvent>(data =>
                queueCollector.Register(new StatDeleted{RuntimeModel = data.RuntimeStat.RuntimeModel.Clone()}));
            
            eventSource.Subscribe<AfterStatChangedEvent>(data =>
                queueCollector.Register(new StatChanged{RuntimeModel = data.RuntimeStat.RuntimeModel.Clone()}));

            #endregion

        }
    }
}