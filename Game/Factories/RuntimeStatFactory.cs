using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Runtime;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Factories
{
    public class RuntimeStatFactory : IRuntimeStatFactory
    {
        private readonly IContext context;

        public RuntimeStatFactory(IContext context)
        {
            this.context = context;
        }
        
        public IRuntimeStatModel CreateModel(int runtimeOwnerId, string ownerId, string dataId)
        {
            if (!context.Database.Stats.TryGet(dataId, out var data))
                throw new NullReferenceException($"{nameof(StatConfig)} with id {dataId}, not found in {nameof(IConfigCollection<StatConfig>)}");
            
            return new RuntimeStatModel
            {
                Id = context.RuntimeIdProvider.Next(),
                ConfigId = dataId,
                OwnerId = ownerId,
                RuntimeOwnerId = runtimeOwnerId,
                Max = data.Max,
                Value = data.Value,
            };
        }

        public IRuntimeStat Create(IRuntimeStatModel runtimeModel, bool notify = true)
        {
            if (!context.ObjectsCollection.TryGet(runtimeModel.RuntimeOwnerId, out var runtimeObject)
                || !context.PlayersCollection.TryGet(runtimeModel.OwnerId, out var runtimePlayer))
                throw new NullReferenceException($"Runtime object with id {runtimeModel.RuntimeOwnerId} with owner {runtimeModel.OwnerId} not found in {nameof(IObjectsCollection)} and {nameof(IPlayersCollection)}");

            var statsCollection = runtimeObject?.StatsCollection ?? runtimePlayer.StatsCollection;
            if (statsCollection.Contains(runtimeModel.Id))
                throw new InvalidOperationException($"Unable create a stat twice : {runtimeModel.AsJsonFormat()}");
            
            if (!context.Database.Stats.TryGet(runtimeModel.ConfigId, out var statData))
                throw new NullReferenceException($"{nameof(StatConfig)} with id {runtimeModel.ConfigId}, not found in {nameof(IConfigCollection<StatConfig>)}");

            var eventSource = runtimeObject?.EventsSource ?? runtimePlayer.EventsSource;
            var eventPublisher = runtimeObject?.EventPublisher ?? runtimePlayer.EventPublisher;
            
            var runtimeStat = new RuntimeStat(statData, runtimeModel, eventPublisher, eventSource);
            statsCollection.Add(runtimeStat, notify);
            
            return runtimeStat;
        }
    }
}