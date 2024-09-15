using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Effects
{
    public readonly struct AfterEffectExecutedEvent
    {
        public IRuntimeEffect RuntimeEffect { get; }

        public AfterEffectExecutedEvent(IRuntimeEffect runtimeEffect)
        {
            RuntimeEffect = runtimeEffect;
        }
    }
}