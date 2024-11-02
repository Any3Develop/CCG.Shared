using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Runtime;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Factories
{
    public class RuntimePlayerFactory : IRuntimePlayerFactory
    {
        private readonly IDatabase database;
        private readonly IRuntimeStatFactory runtimeStatFactory;
        private readonly IPlayersCollection playersCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly IContextFactory contextFactory;

        public RuntimePlayerFactory(
            IDatabase database,
            IPlayersCollection playersCollection,
            IRuntimeIdProvider runtimeIdProvider,
            IRuntimeStatFactory runtimeStatFactory,
            IContextFactory contextFactory)
        {
            this.database = database;
            this.playersCollection = playersCollection;
            this.runtimeIdProvider = runtimeIdProvider;
            this.runtimeStatFactory = runtimeStatFactory;
            this.contextFactory = contextFactory;
        }
        
        public IRuntimePlayerModel Create(int? runtimeId, string ownerId, string dataId = "default-player", bool notify = true)
        {
            if (!database.Players.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(PlayerConfig)} with id {dataId}, not found in {nameof(IConfigCollection<PlayerConfig>)}");

            runtimeId ??= runtimeIdProvider.Next();
            return new RuntimePlayerModel
            {
                Id = runtimeId.Value,
                OwnerId = ownerId,
                ConfigId = dataId,
                Stats = data.Stats.Select(statId => runtimeStatFactory.Create(runtimeId.Value, ownerId, statId, notify)).ToList()
            };
        }

        public IRuntimePlayer Create(IRuntimePlayerModel runtimeModel, bool notify = true)
        {
            if (playersCollection.TryGet(runtimeModel.Id, out var runtimePlayer))
                return runtimePlayer.Sync(runtimeModel, notify);
            
            if (!database.Players.TryGet(runtimeModel.ConfigId, out var config))
                throw new NullReferenceException($"{nameof(PlayerConfig)} with id {runtimeModel.ConfigId}, not found in {nameof(IConfigCollection<PlayerConfig>)}");
            
            var eventSource = contextFactory.CreateEventsSource();
            var eventPublisher = contextFactory.CreateEventPublisher(eventSource);
            var statsCollection = contextFactory.CreateStatsCollection(eventSource);
            
            runtimePlayer = new RuntimePlayer(config, statsCollection, eventPublisher, eventSource).Sync(runtimeModel, notify);
            playersCollection.Add(runtimePlayer, notify);
            
            foreach (var runtimeStatData in runtimeModel.Stats)
                runtimeStatFactory.Create(runtimeStatData, notify);
            
            return runtimePlayer;
        }
    }
}