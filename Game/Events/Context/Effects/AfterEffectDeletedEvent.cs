using CCG.Shared.Abstractions.Game.Runtime.Effects;

namespace CCG.Shared.Game.Events.Context.Effects
{
    public readonly struct AfterEffectDeletedEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public AfterEffectDeletedEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}