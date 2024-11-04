using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Commands;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Game.Commands.Base;

namespace CCG.Shared.Game.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IContext context;
        private readonly ITypeCollection<string, Command> commandTypeCollection;

        public CommandFactory(IContext context, ITypeCollection<string, Command> commandTypeCollection)
        {
            this.context = context;
            this.commandTypeCollection = commandTypeCollection;
        }

        public ICommand Create<T>(ICommandModel model) where T : ICommand
        {
            return Create(typeof(T)).Init(model, context);
        }

        public ICommand Create(ICommandModel model)
        {
            return Create(commandTypeCollection.Get(model.Name)).Init(model, context);
        }

        private static Command Create(Type commandType)
        {
            if (commandType == null)
                throw new NullReferenceException($"Can't create a command instance the type of command is missing.");

            var constructorInfo = commandType.GetConstructor(Type.EmptyTypes);

            if (constructorInfo == null)
                throw new NullReferenceException($"{commandType.FullName} : default constructor not found.");

            return (Command) constructorInfo.Invoke(Array.Empty<object>());
        }
    }
}