using CCG.Shared.Game.Commands.Base;
using CCG.Shared.Game.Events.Context;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Commands
{
    public class InitializeCmd : Command
    {
        protected override void OnExecute()
        {
            Context.EventPublisher.Publish<PlayerInitializeEvent>(Context.PlayersCollection.TryGet(ExecutorId, out var player), player);
        }
    }
}