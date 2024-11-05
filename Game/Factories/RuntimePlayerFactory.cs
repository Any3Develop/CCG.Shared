using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
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
        private readonly IRuntimeStatFactory runtimeStatFactory;
        private readonly ISharedConfig sharedConfig;
        private readonly IPlayersCollection playersCollection;
        private readonly IRuntimeIdProvider runtimeIdProvider;
        private readonly IContextFactory contextFactory;

        public RuntimePlayerFactory(
            ISharedConfig sharedConfig,
            IPlayersCollection playersCollection,
            IRuntimeIdProvider runtimeIdProvider,
            IRuntimeStatFactory runtimeStatFactory,
            IContextFactory contextFactory)
        {
            this.sharedConfig = sharedConfig;
            this.playersCollection = playersCollection;
            this.runtimeIdProvider = runtimeIdProvider;
            this.runtimeStatFactory = runtimeStatFactory;
            this.contextFactory = contextFactory;
        }

        public IRuntimePlayerModel CreateModel(string ownerId, int index)
        {
            return CreateModel(null, ownerId, $"default-player-{index}", false);
        }
        
        public IRuntimePlayerModel CreateModel(int? runtimeId, string ownerId, string dataId, bool notify = true)
        {
            var playerConfig = sharedConfig.Players.FirstOrDefault(x => x.Id == dataId);
            if (playerConfig == null)
                throw new NullReferenceException($"{nameof(PlayerConfig)} with id {dataId}, not found in {nameof(ISharedConfig)}");

            runtimeId ??= runtimeIdProvider.Next();
            return new RuntimePlayerModel
            {
                Id = runtimeId.Value,
                OwnerId = ownerId,
                ConfigId = dataId,
                Stats = playerConfig.Stats.Select(statId => runtimeStatFactory.CreateModel(runtimeId.Value, ownerId, statId, notify)).ToList()
            };
        }

        public IRuntimePlayer Create(IRuntimePlayerModel runtimeModel, bool notify = true)
        {
            if (playersCollection.TryGet(runtimeModel.Id, out var runtimePlayer))
                return runtimePlayer.Sync(runtimeModel);
            
            var playerConfig = sharedConfig.Players.FirstOrDefault(x => x.Id == runtimeModel.ConfigId);
            if (playerConfig == null)
                throw new NullReferenceException($"{nameof(PlayerConfig)} with id {runtimeModel.ConfigId}, not found in {nameof(ISharedConfig)}");
            
            var eventSource = contextFactory.CreateEventsSource();
            var eventPublisher = contextFactory.CreateEventPublisher(eventSource);
            var statsCollection = contextFactory.CreateStatsCollection(eventPublisher);
            
            runtimePlayer = new RuntimePlayer(playerConfig, statsCollection, eventPublisher, eventSource).Sync(runtimeModel);
            playersCollection.Add(runtimePlayer, false);
            
            foreach (var runtimeStatModel in runtimeModel.Stats)
                runtimeStatFactory.Create(runtimeStatModel, false);
            
            if (notify)
                playersCollection.AddNotify(runtimePlayer);
            
            return runtimePlayer;
        }
    }
}