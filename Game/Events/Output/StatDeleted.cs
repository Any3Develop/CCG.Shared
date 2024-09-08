using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Game.Events.Output
{
    public class StatDeleted : GameEvent
    {
        public IRuntimeStatData RuntimeData { get; set; }
    }
}