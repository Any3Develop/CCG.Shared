using CCG.Shared.Abstractions.Game.Commands;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface ICommandFactory
    {
        ICommand Create(string executorId, ICommandModel model);
    }
}