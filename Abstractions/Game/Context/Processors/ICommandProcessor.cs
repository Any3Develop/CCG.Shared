using CCG.Shared.Abstractions.Game.Commands;

namespace CCG.Shared.Abstractions.Game.Context.Processors
{
    public interface ICommandProcessor
    {
        void Execute(ICommandModel model);
    }
}