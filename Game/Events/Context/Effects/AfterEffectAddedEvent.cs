using CCG.Shared.Abstractions.Game.Runtime.Effects;

namespace CCG.Shared.Game.Events.Context.Effects
{
    public readonly struct AfterEffectAddedEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public AfterEffectAddedEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}