using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Args;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime.Effects;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Factories
{
    public class RuntimeEffectFactory : IRuntimeEffectFactory
    {
        private readonly IContext context;
        private readonly ITypeCollection<EffectLogicId, RuntimeEffectBase> logicTypeCollection;

        public RuntimeEffectFactory(
            IContext context,
            ITypeCollection<EffectLogicId, RuntimeEffectBase> logicTypeCollection)
        {
            this.context = context;
            this.logicTypeCollection = logicTypeCollection;
        }

        public IRuntimeEffectModel CreateModel(
            int runtimeOwnerId, 
            string playerOwnerId, 
            string configId, 
            IEnumerable<int> targets, 
            params IRuntimeEffectArg[] args)
        {           
            if (!context.Database.Effects.TryGet(configId, out var data))
                throw new NullReferenceException($"{nameof(EffectConfig)} with id {configId}, not found in {nameof(IConfigCollection<EffectConfig>)}");
            
            return new RuntimeEffectModel // TODO: use logicId to create specified runtime model
            {
                ConfigId = data.Id,
                Id = context.RuntimeIdProvider.Next(),
                OwnerId = playerOwnerId,
                RuntimeOwnerId = runtimeOwnerId,
                Lifetime = data.Lifetime,
                Value = data.Value,
                Args = args.ToList(),
                Targets = targets?.ToList() ?? new List<int>()
            };
        }

        public IRuntimeEffect Create(IRuntimeEffectModel runtimeModel, bool notify = true)
        {
            if (!context.ObjectsCollection.TryGet(runtimeModel.RuntimeOwnerId, out var effectOwner))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeModel.RuntimeOwnerId}, not found in {nameof(IObjectsCollection)}");

            if (effectOwner.EffectsCollection.Contains(runtimeModel.Id))
                throw new InvalidOperationException($"Unable create an effect twice : {runtimeModel.AsJsonFormat()}");
            
            if (!context.Database.Effects.TryGet(runtimeModel.ConfigId, out var data))
                throw new NullReferenceException($"{nameof(EffectConfig)} with id {runtimeModel.ConfigId}, not found in {nameof(IConfigCollection<EffectConfig>)}");
            
            // TODO init effect (context, etc..)
            var runtimeEffect = CreateEffectInstance(data.EffectLogicId).Init(data, runtimeModel, effectOwner.EventPublisher, effectOwner.EventsSource, context);
            effectOwner.EffectsCollection.Add(runtimeEffect, notify);
            
            return runtimeEffect;
        }

        private RuntimeEffectBase CreateEffectInstance(EffectLogicId effectLogicId)
        {
            if (!logicTypeCollection.TryGet(effectLogicId, out var effectType))
                throw new NullReferenceException($"{nameof(Type)} with {nameof(EffectLogicId)} {effectLogicId}, not found in {logicTypeCollection.GetType().Name}");
            
            var constructorInfo = effectType.GetConstructor(Type.EmptyTypes);

            if (constructorInfo == null)
                throw new NullReferenceException($"{effectType.Name} with {nameof(EffectLogicId)} {effectLogicId}, default constructor not found.");
          
            return (RuntimeEffectBase)constructorInfo.Invoke(Array.Empty<object>());
        }
    }
}