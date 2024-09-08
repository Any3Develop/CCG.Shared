using CCG_Shared.Abstractions.Game.Runtime.Objects;

namespace CCG_Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectDeletedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectDeletedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}