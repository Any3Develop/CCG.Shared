using CCG_Shared.Game.Commands.Base;
using CCG_Shared.Game.Events.Context;
using CCG_Shared.Game.Utils;

namespace CCG_Shared.Game.Commands
{
    public class InitializeCmd : Command
    {
        protected override void OnExecute()
        {
            Context.EventPublisher.Publish<PlayerInitializeEvent>(Context.PlayersCollection.TryGet(ExecutorId, out var player), player);
        }
    }
}