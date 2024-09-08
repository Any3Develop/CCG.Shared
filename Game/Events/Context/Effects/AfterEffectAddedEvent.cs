using CCG_Shared.Abstractions.Game.Runtime.Effects;

namespace CCG_Shared.Game.Events.Context.Effects
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