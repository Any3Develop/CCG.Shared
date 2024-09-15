using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Game.Events.Output
{
    public class StatChanged : GameEvent
    {
        public IRuntimeStatData RuntimeData { get; set; }
    }
}