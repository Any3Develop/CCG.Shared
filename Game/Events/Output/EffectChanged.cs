using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class EffectChanged : GameEvent
    {
        public IRuntimeEffectModel RuntimeModel { get; set; }
    }
}