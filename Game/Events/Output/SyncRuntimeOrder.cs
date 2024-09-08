using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Game.Events.Output
{
    public class SyncRuntimeOrder : GameEvent
    {
        public IRuntimeOrderData RuntimeData { get; set; }
    }
}