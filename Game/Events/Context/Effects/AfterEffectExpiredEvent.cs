using CCG_Shared.Abstractions.Game.Runtime.Effects;

namespace CCG_Shared.Game.Events.Context.Effects
{
    public readonly struct AfterEffectExpiredEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public AfterEffectExpiredEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}