using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectSpawnedEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public AfterObjectSpawnedEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}