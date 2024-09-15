using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Abstractions.Game.Runtime.Objects;
using CCG.Shared.Game.Data;
using CCG.Shared.Game.Data.Enums;
using CCG.Shared.Game.Runtime.Cards;
using CCG.Shared.Game.Runtime.Data;

namespace CCG.Shared.Game.Factories
{
    public class RuntimeObjectFactory : IRuntimeObjectFactory
    {
        private readonly IDatabase database;
        private readonly IObjectsCollection objectsCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly IRuntimeStatFactory runtimeStatFactory;
        private readonly IContextFactory contextFactory;

        public RuntimeObjectFactory(
            IDatabase database,
            IObjectsCollection objectsCollection,
            IRuntimeIdProvider runtimeIdProvider,
            IRuntimeStatFactory runtimeStatFactory,
            IContextFactory contextFactory)
        {
            this.database = database;
            this.objectsCollection = objectsCollection;
            this.runtimeIdProvider = runtimeIdProvider;
            this.runtimeStatFactory = runtimeStatFactory;
            this.contextFactory = contextFactory;
        }

        public IRuntimeObjectData Create(int? runtimeId, string ownerId, string dataId, bool notify = true)
        {
            if (!database.Objects.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(ObjectData)} with id {dataId}, not found in {nameof(IDataCollection<ObjectData>)}");

            runtimeId ??= runtimeIdProvider.Next();
            return data.Type switch
            {
                ObjectType.Creature or ObjectType.Spell => new RuntimeCardData
                {
                    DataId = data.Id,
                    Id = runtimeId.Value,
                    OwnerId = ownerId,
                    Stats = data.StatIds.Select(id => runtimeStatFactory.Create(runtimeId.Value, ownerId, id, notify)).ToList(),
                },
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {data.Type}")
            };
        }
        
        public IRuntimeObject Create(IRuntimeObjectData runtimeData, bool notify = true)
        {
            if (objectsCollection.TryGet(runtimeData.Id, out var runtimeObject))
                return runtimeObject.Sync(runtimeData, notify);
            
            if (!database.Objects.TryGet(runtimeData.DataId, out var data))
                throw new NullReferenceException($"{nameof(ObjectData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<ObjectData>)}");
            
            var eventSource = contextFactory.CreateEventsSource();
            var eventPublisher = contextFactory.CreateEventPublisher(eventSource);
            var statsCollection = contextFactory.CreateStatsCollection(eventSource);
            var effectsCollection = contextFactory.CreateEffectsCollection(eventSource);
            runtimeObject = data.Type switch
            {
                ObjectType.Creature => new RuntimeCardCreature().Init(data, runtimeData, statsCollection, effectsCollection, eventPublisher, eventSource),
                ObjectType.Spell => new RuntimeCardSpell().Init(data, runtimeData, statsCollection, effectsCollection, eventPublisher, eventSource),
                _ => throw new NotImplementedException($"Unknown {nameof(ObjectType)}: {data.Type}")
            };
            
            objectsCollection.Add(runtimeObject, notify);
            
            foreach (var runtimeStatData in runtimeData.Stats)
                runtimeStatFactory.Create(runtimeStatData, notify);
            
            return runtimeObject;
        }
    }
}