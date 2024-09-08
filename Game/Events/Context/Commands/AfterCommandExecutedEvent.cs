using CCG_Shared.Abstractions.Game.Commands;

namespace CCG_Shared.Game.Events.Context.Commands
{
    public readonly struct AfterCommandExecutedEvent
    {
        public ICommand Command { get; }
        public AfterCommandExecutedEvent(ICommand command)
        {
            Command = command;
        }
    }
}