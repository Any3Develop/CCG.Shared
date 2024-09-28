using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Commands.Base;
using CCG.Shared.Game.Commands.Models;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Exceptions;

namespace CCG.Shared.Game.Commands
{
    public class PlayCardCmd : Command<PlayCardModel>
    {
        protected override void OnExecute()
        {
            if (!Context.PlayersCollection.Contains(ExecutorId))
                throw new NullReferenceException("Player who executed the command wasn't found.");
            
            if (!Context.ObjectsCollection.TryGet<IRuntimeCard>(Model.Id, out var runtimeCard))
                throw new NullReferenceException($"Requested card with id {Model.Id} not found.");

            if (Context.ObjectsCollection.GetOccupiedTableSpace(ExecutorId) >= Context.Config.Table.MaxInTableCount)
                throw new NotEnoughTableSpaceException();
            
            runtimeCard.SetPosition(Model.Position);
            runtimeCard.SetState(ObjectState.InTable);
        }
    }
}