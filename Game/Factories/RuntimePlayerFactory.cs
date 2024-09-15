using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Abstractions.Game.Runtime.Players;
using CCG.Shared.Game.Data;
using CCG.Shared.Game.Runtime.Data;
using CCG.Shared.Game.Runtime.Players;

namespace CCG.Shared.Game.Factories
{
    public class RuntimePlayerFactory : IRuntimePlayerFactory
    {
        private readonly IDatabase database;
        private readonly IRuntimeStatFactory runtimeStatFactory;
        private readonly IContextFactory contextFactory;
        private readonly IPlayersCollection playersCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;

        public RuntimePlayerFactory(
            IDatabase database,
            IRuntimeStatFactory runtimeStatFactory,
            IContextFactory contextFactory,
            IPlayersCollection playersCollection,
            IRuntimeIdProvider runtimeIdProvider)
        {
            this.database = database;
            this.runtimeStatFactory = runtimeStatFactory;
            this.contextFactory = contextFactory;
            this.playersCollection = playersCollection;
            this.runtimeIdProvider = runtimeIdProvider;
        }
        
        public IRuntimePlayerData Create(int? runtimeId, string ownerId, string dataId = "default-player", bool notify = true)
        {
            if (!database.Players.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(PlayerData)} with id {dataId}, not found in {nameof(IDataCollection<PlayerData>)}");

            runtimeId ??= runtimeIdProvider.Next();
            return new RuntimePlayerData
            {
                Id = runtimeId.Value,
                OwnerId = ownerId,
                DataId = dataId,
                Stats = data.StatIds.Select(statId => runtimeStatFactory.Create(runtimeId.Value, ownerId, statId, notify)).ToList()
            };
        }

        public IRuntimePlayer Create(IRuntimePlayerData runtimeData, bool notify = true)
        {
            if (playersCollection.TryGet(runtimeData.Id, out var runtimePlayer))
                return runtimePlayer.Sync(runtimeData, notify);
            
            var eventSource = contextFactory.CreateEventsSource();
            var eventPublisher = (IEventPublisher)eventSource;
            var statsCollection = contextFactory.CreateStatsCollection(eventSource);
            runtimePlayer = new RuntimePlayer(statsCollection, eventPublisher, eventSource).Sync(runtimeData, notify);
            playersCollection.Add(runtimePlayer, notify);
            
            foreach (var runtimeStatData in runtimeData.Stats)
                runtimeStatFactory.Create(runtimeStatData, notify);
            
            return runtimePlayer;
        }
    }
}