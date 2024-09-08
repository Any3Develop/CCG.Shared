﻿using Shared.Abstractions.Game.Runtime.Objects;

namespace Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectStateChangedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectStateChangedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}