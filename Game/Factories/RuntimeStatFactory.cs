using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Runtime;
using CCG.Shared.Game.Runtime.Models;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Factories
{
    public class RuntimeStatFactory : IRuntimeStatFactory
    {
        private readonly IDatabase database;
        private readonly IObjectsCollection objectsCollection;
        private readonly IPlayersCollection playersCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;

        public RuntimeStatFactory(
            IDatabase database,
            IObjectsCollection objectsCollection,
            IPlayersCollection playersCollection,
            IRuntimeIdProvider runtimeIdProvider)
        {
            this.database = database;
            this.objectsCollection = objectsCollection;
            this.playersCollection = playersCollection;
            this.runtimeIdProvider = runtimeIdProvider;
        }

        public IRuntimeStatModel CreateModel(int? runtimeOwnerId, string ownerId, string dataId, bool notify = true)
        {
            if (!runtimeOwnerId.HasValue)
                throw new NullReferenceException($"To create {nameof(IRuntimeStat)} you should inject {nameof(runtimeOwnerId)}");
            
            if (!database.Stats.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(StatConfig)} with id {dataId}, not found in {nameof(IConfigCollection<StatConfig>)}");
            
            return new RuntimeStatModel
            {
                Id = runtimeIdProvider.Next(),
                ConfigId = dataId,
                OwnerId = ownerId,
                Type = data.Type,
                RuntimeOwnerId = runtimeOwnerId.Value,
                Max = data.Max,
                Value = data.Value,
            };
        }

        public IRuntimeStat Create(IRuntimeStatModel runtimeModel, bool notify = true)
        {
            if (!objectsCollection.TryGet(runtimeModel.RuntimeOwnerId, out var runtimeObject)
                || !playersCollection.TryGet(runtimeModel.OwnerId, out var runtimePlayer))
                throw new NullReferenceException($"Runtime object with id {runtimeModel.RuntimeOwnerId} with owner {runtimeModel.OwnerId} not found in {nameof(IObjectsCollection)} and {nameof(IPlayersCollection)}");

            var statsCollection = runtimeObject?.StatsCollection ?? runtimePlayer.StatsCollection;
            if (statsCollection.Contains(runtimeModel.Id))
                throw new InvalidOperationException($"Unable create a stat twice : {runtimeModel.ReflectionFormat()}");
            
            if (!database.Stats.TryGet(runtimeModel.ConfigId, out var statData))
                throw new NullReferenceException($"{nameof(StatConfig)} with id {runtimeModel.ConfigId}, not found in {nameof(IConfigCollection<StatConfig>)}");

            var eventSource = runtimeObject?.EventsSource ?? runtimePlayer.EventsSource;
            var eventPublisher = runtimeObject?.EventPublisher ?? runtimePlayer.EventPublisher;
            
            var runtimeStat = new RuntimeStat(statData, runtimeModel, eventPublisher, eventSource);
            statsCollection.Add(runtimeStat, notify);
            
            return runtimeStat;
        }
    }
}