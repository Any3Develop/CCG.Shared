using CCG_Shared.Abstractions.Game.Runtime.Objects;

namespace CCG_Shared.Game.Events.Context.Objects
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