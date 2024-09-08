using CCG_Shared.Abstractions.Game.Collections;
using CCG_Shared.Abstractions.Game.Commands;
using CCG_Shared.Abstractions.Game.Context;
using CCG_Shared.Abstractions.Game.Factories;
using CCG_Shared.Game.Commands.Base;

namespace CCG_Shared.Game.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IContext context;
        private readonly ITypeCollection<string> commandTypeCollection;

        public CommandFactory(IContext context, ITypeCollection<string> commandTypeCollection)
        {
            this.context = context;
            this.commandTypeCollection = commandTypeCollection;
        }
        
        public ICommand Create<T>(string executorId, ICommandModel model) where T : ICommand
        {
            var cmd = Create(typeof(T));
            cmd.Init(executorId, model, context);
            return cmd;
        }

        public ICommand Create(string executorId, ICommandModel model)
        {
            var cmd = Create(commandTypeCollection.Get(model.TypeName));
            cmd.Init(executorId, model, context);
            return cmd;
        }
        
        private static Command Create(Type commandType)
        {
            if (commandType == null)
                throw new NullReferenceException($"Can't create a command instance the type of command is missing.");
            
            var constructorInfo = commandType.GetConstructor(Type.EmptyTypes);

            if (constructorInfo == null)
                throw new NullReferenceException($"{commandType.FullName} : default constructor not found.");
          
            return (Command)constructorInfo.Invoke(Array.Empty<object>());
        }
    }
}