using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Game.Events.Output
{
    public class SyncRuntimeId : GameEvent
    {
        public IRuntimeIdData RuntimeData { get; set; }
    }
}