using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Objects
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