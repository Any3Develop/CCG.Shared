using CCG.Shared.Abstractions.Game.Commands;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Commands.Base
{
    public abstract class Command<T> : Command, ICommand<T> where T : ICommandModel
    {
        public new T Model => (T)base.Model;
    }
    
    public abstract class Command : ICommand
    {
        public ICommandModel Model { get; private set; }
        protected IContext Context { get; private set; }

        public ICommand Init(ICommandModel model, IContext context)
        {
            Model = model;
            Context = context;
            return this;
        }
        
        public void Execute()
        {
            OnExecute();
        }

        protected abstract void OnExecute();

        protected bool TryGetExecutor(out IRuntimePlayer result)
            => Context.PlayersCollection.TryGet(Model.ExecutorId, out result);
        protected bool TryGetOpposite(out IRuntimePlayer result) 
            => (result = Context.PlayersCollection.GetOpposite(Model.ExecutorId)) != null;
    }
}