using CCG.Shared.Abstractions.Game.Commands;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface ICommandFactory
    {
        ICommand Create<T>(ICommandModel model) where T : ICommand;
        ICommand Create(ICommandModel model);
    }
}