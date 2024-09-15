using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Objects
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