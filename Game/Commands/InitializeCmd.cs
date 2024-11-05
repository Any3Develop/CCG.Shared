using CCG.Shared.Game.Commands.Base;
using CCG.Shared.Game.Events.Context;

namespace CCG.Shared.Game.Commands
{
    public class InitializeCmd : Command
    {
        protected override void OnExecute()
        {
            if (TryGetExecutor(out var player))
                Context.EventPublisher.Publish(new PlayerInitializeEvent(player));
        }
    }
}