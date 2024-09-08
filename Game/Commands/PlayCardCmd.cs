using CCG_Shared.Abstractions.Game.Runtime.Cards;
using CCG_Shared.Game.Commands.Base;
using CCG_Shared.Game.Commands.Models;
using CCG_Shared.Game.Data.Enums;
using CCG_Shared.Game.Exceptions;

namespace CCG_Shared.Game.Commands
{
    public class PlayCardCmd : Command<PlayCardModel>
    {
        protected override void OnExecute()
        {
            if (!Context.PlayersCollection.Contains(ExecutorId))
                throw new NullReferenceException("Player who executed the command wasn't found.");
            
            if (!Context.ObjectsCollection.TryGet<IRuntimeCard>(Model.Id, out var runtimeCard))
                throw new NullReferenceException($"Requested card with id {Model.Id} not found.");

            if (Context.ObjectsCollection.GetOccupiedTableSpace(ExecutorId) >= Context.Config.MaxInTableCount)
                throw new NotEnoughTableSpaceException();
            
            runtimeCard.SetPosition(Model.Position);
            runtimeCard.SetState(ObjectState.InTable);
        }
    }
}