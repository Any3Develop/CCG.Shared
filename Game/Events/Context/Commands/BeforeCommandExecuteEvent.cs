using CCG_Shared.Abstractions.Game.Commands;

namespace CCG_Shared.Game.Events.Context.Commands
{
    public readonly struct BeforeCommandExecuteEvent
    {
        public ICommand Command { get; }
        public BeforeCommandExecuteEvent(ICommand command)
        {
            Command = command;
        }
    }
}