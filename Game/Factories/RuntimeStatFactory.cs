using CCG_Shared.Abstractions.Game.Collections;
using CCG_Shared.Abstractions.Game.Context;
using CCG_Shared.Abstractions.Game.Context.Providers;
using CCG_Shared.Abstractions.Game.Factories;
using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Abstractions.Game.Runtime.Objects;
using CCG_Shared.Abstractions.Game.Runtime.Stats;
using CCG_Shared.Game.Data;
using CCG_Shared.Game.Runtime.Data;
using CCG_Shared.Game.Runtime.Stats;

namespace CCG_Shared.Game.Factories
{
    public class RuntimeStatFactory : IRuntimeStatFactory
    {
        private readonly IDatabase database;
        private readonly IObjectsCollection objectsCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;

        public RuntimeStatFactory(
            IDatabase database,
            IObjectsCollection objectsCollection,
            IRuntimeIdProvider runtimeIdProvider)
        {
            this.database = database;
            this.objectsCollection = objectsCollection;
            this.runtimeIdProvider = runtimeIdProvider;
        }

        public IRuntimeStatData Create(int? runtimeOwnerId, string ownerId, string dataId, bool notify = true)
        {
            if (!runtimeOwnerId.HasValue)
                throw new NullReferenceException($"To create {nameof(IRuntimeStat)} you should inject {nameof(runtimeOwnerId)}");
            
            if (!database.Stats.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(StatData)} with id {dataId}, not found in {nameof(IDataCollection<StatData>)}");
            
            return new RuntimeStatData
            {
                Id = runtimeIdProvider.Next(),
                DataId = dataId,
                OwnerId = ownerId,
                RuntimeOwnerId = runtimeOwnerId.Value,
                Max = data.Max,
                Value = data.Value,
            };
        }

        public IRuntimeStat Create(IRuntimeStatData runtimeData, bool notify = true)
        {
            if (!objectsCollection.TryGet(runtimeData.RuntimeOwnerId, out var runtimeObject))
                throw new NullReferenceException($"{nameof(IRuntimeObject)} with id {runtimeData.Id}, not found in {nameof(IObjectsCollection)}");

            if (runtimeObject.StatsCollection.TryGet(runtimeData.Id, out var runtimeStat))
                return runtimeStat.Sync(runtimeData, notify);
            
            if (!database.Stats.TryGet(runtimeData.DataId, out var statData))
                throw new NullReferenceException($"{nameof(StatData)} with id {runtimeData.DataId}, not found in {nameof(IDataCollection<StatData>)}");
            
            runtimeStat = new RuntimeStat().Init(statData, runtimeData, runtimeObject.EventPublisher, runtimeObject.EventsSource);
            runtimeObject.StatsCollection.Add(runtimeStat, notify);
            
            return runtimeStat;
        }
    }
}