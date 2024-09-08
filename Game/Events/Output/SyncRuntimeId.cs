using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Game.Events.Output
{
    public class SyncRuntimeId : GameEvent
    {
        public IRuntimeIdData RuntimeData { get; set; }
    }
}