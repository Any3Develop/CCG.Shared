using CCG.Shared.Common.Logger;
using CCG.Shared.Game.Commands.Base;
using CCG.Shared.Game.Events.Context;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Commands
{
    public class PlayerReadyCmd : Command
    {
        protected override void OnExecute()
        {
            if (!TryGetExecutor(out var player))
                throw new NullReferenceException("Player who executed the command wasn't found.");

            if (player.RuntimeModel.Ready)
            {
                SharedLogger.Log($"Player {player.RuntimeModel.Name} already ready.");
                return;
            }
            
            player.SetReady(true);
            Context.EventPublisher.Publish<SessionStartEvent>(Context.PlayersCollection.All(x => x.RuntimeModel.Ready));
        }
    }
}