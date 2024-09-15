using CCG.Shared.Abstractions.Game.Runtime;

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