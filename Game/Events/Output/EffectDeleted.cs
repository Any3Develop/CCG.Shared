using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Game.Events.Output
{
    public class EffectDeleted : GameEvent
    {
        public IRuntimeEffectData RuntimeData { get; set; }
    }
}