using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Objects
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