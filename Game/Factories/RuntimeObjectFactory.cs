using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime;
using CCG.Shared.Game.Runtime.Cards;
using CCG.Shared.Game.Runtime.Heroes;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Factories
{
    public class RuntimeObjectFactory : IRuntimeObjectFactory
    {
        private readonly IDatabase database;
        private readonly IObjectsCollection objectsCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly IRuntimeStatFactory runtimeStatFactory;
        private readonly IRuntimeEffectFactory runtimeEffectFactory;
        private readonly IContextFactory contextFactory;

        public RuntimeObjectFactory(
            IDatabase database,
            IObjectsCollection objectsCollection,
            IRuntimeIdProvider runtimeIdProvider,
            IRuntimeStatFactory runtimeStatFactory,
            IRuntimeEffectFactory runtimeEffectFactory,
            IContextFactory contextFactory)
        {
            this.database = database;
            this.objectsCollection = objectsCollection;
            this.runtimeIdProvider = runtimeIdProvider;
            this.runtimeStatFactory = runtimeStatFactory;
            this.runtimeEffectFactory = runtimeEffectFactory;
            this.contextFactory = contextFactory;
        }

        public IRuntimeObjectModel CreateModel(string ownerId, string dataId)
        {
            if (!database.Objects.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(ObjectConfig)} with id {dataId}, not found in {nameof(IConfigCollection<ObjectConfig>)}");

            var runtimeId = runtimeIdProvider.Next();
            return data.Type switch
            {
                ObjectType.Creature or ObjectType.Spell => new RuntimeCardModel
                {
                    ConfigId = data.Id,
                    Id = runtimeId,
                    OwnerId = ownerId,
                    Stats = data.Stats
                        .Select(id => runtimeStatFactory.CreateModel(runtimeId, ownerId, id))
                        .ToList(),
                    State = ObjectState.Created
                },
                
                ObjectType.Hero => new RuntimeHeroModel // TODO some special data
                {
                    ConfigId = data.Id,
                    Id = runtimeId,
                    OwnerId = ownerId,
                    Stats = data.Stats
                        .Select(id => runtimeStatFactory.CreateModel(runtimeId, ownerId, id))
                        .ToList(),
                    State = ObjectState.Created
                },
                
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {data.Type}")
            };
        }

        public IRuntimeObject Create(IRuntimeObjectModel runtimeModel, bool notify = false)
        {
            var runtimeObject = CreateInternal(runtimeModel);
            InitInternal(runtimeObject, false);
            
            if (notify)
                objectsCollection.AddNotify(runtimeObject);
            
            return runtimeObject;
        }
        
        public void Restore(IEnumerable<IRuntimeObjectModel> runtimeModels)
        {
            foreach (var runtimeObject in runtimeModels.Reverse().Select(CreateInternal))
                InitInternal(runtimeObject, true);
        }

        private IRuntimeObject CreateInternal(IRuntimeObjectModel runtimeModel)
        {
            if (objectsCollection.Contains(runtimeModel.Id))
                throw new InvalidOperationException($"Unable create an object twice : {runtimeModel.AsJsonFormat()}");
            
            if (!database.Objects.TryGet(runtimeModel.ConfigId, out var data))
                throw new NullReferenceException($"{nameof(ObjectConfig)} with id {runtimeModel.ConfigId}, not found in {nameof(IConfigCollection<ObjectConfig>)}");
            
            var eventSource = contextFactory.CreateEventsSource();
            var eventPublisher = contextFactory.CreateEventPublisher(eventSource);
            var statsCollection = contextFactory.CreateStatsCollection(eventPublisher);
            var effectsCollection = contextFactory.CreateEffectsCollection(eventPublisher);
            
            RuntimeObjectBase runtimeObject = data.Type switch
            {
                ObjectType.Creature => new RuntimeCardCreature(),
                ObjectType.Spell => new RuntimeCardSpell(),
                ObjectType.Hero => new RuntimeHero(),
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {data.Type}")
            };

            runtimeObject.Init(data, runtimeModel, statsCollection, effectsCollection, eventPublisher, eventSource);
            objectsCollection.Add(runtimeObject, false);
            
            return runtimeObject;
        }

        private void InitInternal(IRuntimeObject runtimeObject, bool reversed)
        {
            var statModels = reversed
                ? runtimeObject.RuntimeModel.Stats.AsEnumerable().Reverse()
                : runtimeObject.RuntimeModel.Stats;
            
            foreach (var runtimeStatModel in statModels)
                runtimeStatFactory.Create(runtimeStatModel, false);
            
            var effectModels = reversed
                ? runtimeObject.RuntimeModel.Applied.AsEnumerable().Reverse()
                : runtimeObject.RuntimeModel.Applied;
            
            foreach (var runtimeEffectModel in effectModels)
                runtimeEffectFactory.Create(runtimeEffectModel, false);
        }
    }
}