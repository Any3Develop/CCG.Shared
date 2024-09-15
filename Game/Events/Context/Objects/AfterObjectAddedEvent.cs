using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Objects
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