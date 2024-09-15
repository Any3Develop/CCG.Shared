using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Effects
{
    public readonly struct BeforeEffectExpireEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public BeforeEffectExpireEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}