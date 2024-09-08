using CCG_Shared.Abstractions.Game.Runtime.Objects;

namespace CCG_Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectAddedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectAddedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}