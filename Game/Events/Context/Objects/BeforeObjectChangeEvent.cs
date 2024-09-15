using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Objects
{
    public readonly struct BeforeObjectChangeEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public BeforeObjectChangeEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}