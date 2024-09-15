﻿using CCG.Shared.Abstractions.Game.Commands;

namespace CCG.Shared.Game.Events.Context.Commands
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