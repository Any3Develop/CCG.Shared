﻿using CCG_Shared.Abstractions.Game.Commands;
using CCG_Shared.Abstractions.Game.Context;
using CCG_Shared.Abstractions.Game.Factories;
using CCG_Shared.Common.Logger;
using CCG_Shared.Game.Events.Context.Commands;

namespace CCG_Shared.Game.Context
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IContext context;
        private readonly ICommandFactory commandFactory;

        public CommandProcessor(
            IContext context, 
            ICommandFactory commandFactory)
        {
            this.context = context;
            this.commandFactory = commandFactory;
        }
        
        public void Execute(string executorId, ICommandModel model)
        {
            Execute(commandFactory.Create(executorId, model));
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