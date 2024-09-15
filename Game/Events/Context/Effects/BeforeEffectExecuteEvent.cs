using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Effects
{
    public readonly struct BeforeEffectExecuteEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public BeforeEffectExecuteEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}