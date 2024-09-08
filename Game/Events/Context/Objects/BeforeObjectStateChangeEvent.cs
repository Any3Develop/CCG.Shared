using CCG_Shared.Abstractions.Game.Runtime.Objects;

namespace CCG_Shared.Game.Events.Context.Objects
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