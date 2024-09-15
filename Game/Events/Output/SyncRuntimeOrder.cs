using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Game.Events.Output
{
    public class SyncRuntimeOrder : GameEvent
    {
        public IRuntimeOrderData RuntimeData { get; set; }
    }
}