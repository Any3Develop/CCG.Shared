﻿using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventProcessors;
using CCG.Shared.Abstractions.Game.Events;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Events.Output;

namespace CCG.Shared.Game.Context.EventProcessors
{
    public class GameEventProcessor : IGameEventProcessor
    {
        private readonly IContext context;

        public GameEventProcessor(IContext context)
        {
            this.context = context;
        }

        public void Process(IGameEvent gameEvent)
        {
            switch (gameEvent)
            {
                case EffectAdded addedObjectEffect:
                {
                    context.EffectFactory.Create(addedObjectEffect.RuntimeModel, false);
                    return;
                }

                case StatAdded addedObjectStat:
                {
                    context.StatFactory.Create(addedObjectStat.RuntimeModel, false);
                    return;
                }

                case EffectChanged changedObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectEffect.RuntimeModel.EffectOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(changedObjectEffect.RuntimeModel.Id, out var runtimeEffect))
                        return;

                    runtimeEffect.Sync(changedObjectEffect.RuntimeModel, false);
                    return;
                }
                
                case CardPositionChanged changedObjectPosition:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectPosition.Id, out IRuntimeCard runtimeCard))
                        return;
                    
                    runtimeCard.SetPosition(changedObjectPosition.Position, false);
                    return;
                }
                
                case StatChanged changedObjectStat:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectStat.RuntimeModel.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.StatsCollection.TryGet(changedObjectStat.RuntimeModel.Id, out var runtimeStat))
                        return;
                    
                    runtimeStat.Sync(changedObjectStat.RuntimeModel, false);
                    return;
                }
                
                case ObjectStateChanged changedObjectState:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectState.Id, out var runtimeObject))
                        return;
                    
                    runtimeObject.SetState(changedObjectState.State, false);
                    return;
                }
                
                case ObjectDeleted deletedObject:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObject.RuntimeModel.Id, out var runtimeObject))
                        return;
                    
                    context.ObjectsCollection.Remove(runtimeObject, false);
                    runtimeObject.Dispose();
                    return;
                }
                
                case EffectDeleted deletedObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObjectEffect.RuntimeModel.EffectOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(deletedObjectEffect.RuntimeModel.Id, out var runtimeEffect))
                        return;

                    runtimeObject.EffectsCollection.Remove(runtimeEffect, false);
                    runtimeEffect.Dispose();
                    return;
                }
                
                case StatDeleted deletedObjectStat:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObjectStat.RuntimeModel.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.StatsCollection.TryGet(deletedObjectStat.RuntimeModel.Id, out var runtimeStat))
                        return;

                    runtimeObject.StatsCollection.Remove(runtimeStat, false);
                    runtimeStat.Dispose();
                    return;
                }
                
                case EffectEnded endedObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(endedObjectEffect.RuntimeModel.EffectOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(endedObjectEffect.RuntimeModel.Id, out var runtimeEffect))
                        return;
                    
                    runtimeEffect.Sync(endedObjectEffect.RuntimeModel, false);
                    return;
                }
                
                case AddedObject addedObject:
                {
                    context.ObjectFactory.Create(addedObject.RuntimeModel, false);
                    return;
                }
                
                case EffectStarted startObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(startObjectEffect.RuntimeModel.EffectOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(startObjectEffect.RuntimeModel.Id, out var runtimeEffect))
                        return;
                    
                    runtimeEffect.Sync(startObjectEffect.RuntimeModel, false);
                    return;
                }
                
                case SyncRuntimeId syncRuntimeId:
                {
                    context.RuntimeIdProvider.Sync(syncRuntimeId.RuntimeModel);
                    return;
                }
                
                case SyncRuntimeOrder syncRuntimeOrder:
                {
                    context.RuntimeOrderProvider.Sync(syncRuntimeOrder.RuntimeModel);
                    return;
                }
                
                case SyncRuntimeRandom syncRuntimeRandom: 
                    context.RuntimeRandomProvider.Sync(syncRuntimeRandom.RuntimeModel); break;

                default: throw new NotImplementedException($"{GetType().Name}, Unknown {nameof(IGameEvent)} '{gameEvent?.GetType().FullName}'");
            }
        }
    }
}