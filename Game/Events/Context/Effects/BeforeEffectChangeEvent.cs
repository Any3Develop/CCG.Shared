using CCG.Shared.Abstractions.Game.Runtime.Effects;

namespace CCG.Shared.Game.Events.Context.Effects
{
    public readonly struct BeforeEffectChangeEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public BeforeEffectChangeEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}