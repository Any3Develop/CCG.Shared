using CCG_Shared.Abstractions.Game.Runtime.Objects;

namespace CCG_Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectChangedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectChangedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}