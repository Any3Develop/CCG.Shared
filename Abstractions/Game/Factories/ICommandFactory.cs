using CCG_Shared.Abstractions.Game.Commands;

namespace CCG_Shared.Abstractions.Game.Factories
{
    public interface ICommandFactory
    {
        ICommand Create(string executorId, ICommandModel model);
    }
}