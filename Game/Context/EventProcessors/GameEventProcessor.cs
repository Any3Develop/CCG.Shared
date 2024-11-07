using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventProcessors;
using CCG.Shared.Abstractions.Game.Events;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Events.Output;
using CCG.Shared.Game.Utils;

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
                    if (!context.ObjectsCollection.TryGet(changedObjectEffect.RuntimeModel.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(changedObjectEffect.RuntimeModel.Id, out var runtimeEffect))
                        return;

                    runtimeEffect.Sync(changedObjectEffect.RuntimeModel);
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
                    
                    runtimeStat.Sync(changedObjectStat.RuntimeModel);
                    return;
                }
                
                case ObjectStateChanged changedObjectState:
                {
                    if (!context.ObjectsCollection.TryGet(changedObjectState.Id, out var runtimeObject))
                        return;
                    
                    runtimeObject.SetState(changedObjectState.State, changedObjectState.Previous, false);
                    return;
                }
                
                case ObjectDeleted deletedObject:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObject.RuntimeModel.Id, out var runtimeObject))
                        return;

                    runtimeObject.Sync(deletedObject.RuntimeModel);
                    if(!context.ObjectsCollection.Remove(runtimeObject, false))
                        throw new ApplicationException($"Object hasn't removed : {runtimeObject.RuntimeModel.ReflectionFormat()}");
                    
                    runtimeObject.Dispose();
                    return;
                }
                
                case EffectDeleted deletedObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObjectEffect.RuntimeModel.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(deletedObjectEffect.RuntimeModel.Id, out var runtimeEffect))
                        return;

                    runtimeEffect.Sync(deletedObjectEffect.RuntimeModel);
                    runtimeObject.EffectsCollection.Replace(runtimeEffect);
                    if(!runtimeObject.EffectsCollection.Remove(runtimeEffect, false))
                        throw new ApplicationException($"Effect hasn't removed : {runtimeEffect.RuntimeModel.ReflectionFormat()}");
                    
                    runtimeEffect.Dispose();
                    return;
                }
                
                case StatDeleted deletedObjectStat:
                {
                    if (!context.ObjectsCollection.TryGet(deletedObjectStat.RuntimeModel.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.StatsCollection.TryGet(deletedObjectStat.RuntimeModel.Id, out var runtimeStat))
                        return;

                    runtimeStat.Sync(deletedObjectStat.RuntimeModel);
                    if(!runtimeObject.StatsCollection.Remove(runtimeStat, false))
                        throw new ApplicationException($"Stat hasn't removed : {runtimeStat.RuntimeModel.ReflectionFormat()}");
                    
                    runtimeStat.Dispose();
                    return;
                }
                
                case EffectEnded endedObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(endedObjectEffect.RuntimeModel.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(endedObjectEffect.RuntimeModel.Id, out var runtimeEffect))
                        return;
                    
                    runtimeEffect.Sync(endedObjectEffect.RuntimeModel);
                    return;
                }
                
                case AddedObject addedObject:
                {
                    context.ObjectFactory.Create(addedObject.RuntimeModel, false);
                    return;
                }
                
                case EffectStarted startObjectEffect:
                {
                    if (!context.ObjectsCollection.TryGet(startObjectEffect.RuntimeModel.RuntimeOwnerId, out var runtimeObject)
                        || !runtimeObject.EffectsCollection.TryGet(startObjectEffect.RuntimeModel.Id, out var runtimeEffect))
                        return;
                    
                    runtimeEffect.Sync(startObjectEffect.RuntimeModel);
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
                {
                    context.RuntimeRandomProvider.Sync(syncRuntimeRandom.RuntimeModel);
                    return;
                }

                default: throw new NotImplementedException($"{GetType().Name}, Unknown {nameof(IGameEvent)} '{gameEvent?.GetType().FullName}'");
            }
        }
    }
}