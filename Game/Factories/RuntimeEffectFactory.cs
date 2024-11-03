using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime.Effects;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Factories
{
    public class RuntimeEffectFactory : IRuntimeEffectFactory
    {
        private readonly IDatabase database;
        private readonly IObjectsCollection objectsCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly ITypeCollection<LogicId, RuntimeEffectBase> logicTypeCollection;

        public RuntimeEffectFactory(
            IDatabase database, 
            IObjectsCollection objectsCollection,
            IRuntimeIdProvider runtimeIdProvider,
            ITypeCollection<LogicId, RuntimeEffectBase> logicTypeCollection)
        {
            this.database = database;
            this.objectsCollection = objectsCollection;
            this.runtimeIdProvider = runtimeIdProvider;
            this.logicTypeCollection = logicTypeCollection;
        }

        public IRuntimeEffectModel CreateModel(int? runtimeId, string ownerId, string dataId, bool notify = true)
        {           
            if (!database.Effects.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(EffectConfig)} with id {dataId}, not found in {nameof(IConfigCollection<EffectConfig>)}");
            
            return new RuntimeEffectModel // TODO: use keyword to create specified runtime data
            {
                ConfigId = data.Id,
                Id = runtimeId ?? runtimeIdProvider.Next(),
                OwnerId = ownerId,
                Lifetime = data.Lifetime,
                Value = data.Value
            };
        }

        public IRuntimeEffect Create(IRuntimeEffectModel runtimeModel, bool notify = true)
        {
            if (!objectsCollection.TryGet(runtimeModel.RuntimeOwnerId, out var runtimeEffectOwnerObject))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeModel.RuntimeOwnerId}, not found in {nameof(IObjectsCollection)}");

            if (runtimeEffectOwnerObject.EffectsCollection.TryGet(runtimeModel.Id, out var runtimeEffect))
                return runtimeEffect.Sync(runtimeModel, notify);
            
            if (!database.Effects.TryGet(runtimeModel.ConfigId, out var data))
                throw new NullReferenceException($"{nameof(EffectConfig)} with id {runtimeModel.ConfigId}, not found in {nameof(IConfigCollection<EffectConfig>)}");
            
            runtimeEffect = CreateEffectInstance(data.LogicId).Init(data, runtimeModel, runtimeEffectOwnerObject.EventPublisher, runtimeEffectOwnerObject.EventsSource);
            runtimeEffectOwnerObject.EffectsCollection.Add(runtimeEffect, notify);
            
            return runtimeEffect;
        }

        private RuntimeEffectBase CreateEffectInstance(LogicId logicId)
        {
            if (!logicTypeCollection.TryGet(logicId, out var effectType))
                throw new NullReferenceException($"{nameof(Type)} with {nameof(LogicId)} {logicId}, not found in {logicTypeCollection.GetType().Name}");
            
            var constructorInfo = effectType.GetConstructor(Type.EmptyTypes);

            if (constructorInfo == null)
                throw new NullReferenceException($"{effectType.Name} with {nameof(LogicId)} {logicId}, default constructor not found.");
          
            return (RuntimeEffectBase)constructorInfo.Invoke(Array.Empty<object>());
        }
    }
}