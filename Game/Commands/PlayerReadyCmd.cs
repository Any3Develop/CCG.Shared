using CCG_Shared.Common.Logger;
using CCG_Shared.Game.Commands.Base;
using CCG_Shared.Game.Events.Context;
using CCG_Shared.Game.Utils;

namespace CCG_Shared.Game.Commands
{
    public class PlayerReadyCmd : Command
    {
        protected override void OnExecute()
        {
            if (!Context.PlayersCollection.TryGet(ExecutorId, out var player))
                throw new NullReferenceException("Player who executed the command wasn't found.");

            if (player.RuntimeData.Ready)
            {
                SharedLogger.Log($"Player {ExecutorId} already ready.");
                return;
            }
            
            player.SetReady(true);
            Context.EventPublisher.Publish<SessionStartEvent>(Context.PlayersCollection.All(x => x.RuntimeData.Ready));
        }
    }
}