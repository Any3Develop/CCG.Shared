using CCG.Shared.Abstractions.Game.Commands;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Common.Logger;
using CCG.Shared.Game.Events.Context.Commands;

namespace CCG.Shared.Game.Context.Processors
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IContext context;

        public CommandProcessor(IContext context)
        {
            this.context = context;
        }
        
        public void Execute(ICommandModel model)
        {
            Execute(context.CommandFactory.Create(model));
        }

        private void Execute(ICommand command)
        {
            try
            {
                context.EventPublisher.Publish(new BeforeCommandExecuteEvent(command));
                command.Execute();
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
            }
            finally
            {
                // moved here to prevent interrupt event by command throw
                context.EventPublisher.Publish(new AfterCommandExecutedEvent(command)); 
                
                if (!command.Model.IsNested)
                    context.GameQueueCollector.Release();
            }
        }
    }
}