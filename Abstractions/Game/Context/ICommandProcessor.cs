using CCG_Shared.Abstractions.Game.Commands;

namespace CCG_Shared.Abstractions.Game.Context
{
    public interface ICommandProcessor
    {
        void Execute(string executorId, ICommandModel model);
    }
}