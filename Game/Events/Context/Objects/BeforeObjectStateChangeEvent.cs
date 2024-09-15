using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Objects
{
    public readonly struct BeforeObjectStateChangeEvent
    {
        public IRuntimeObject RuntimeObject { get; }

        public BeforeObjectStateChangeEvent(IRuntimeObject runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}