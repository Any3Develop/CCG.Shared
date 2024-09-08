using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Game.Events.Output
{
    public class EffectChanged : GameEvent
    {
        public IRuntimeEffectData RuntimeData { get; set; }
    }
}