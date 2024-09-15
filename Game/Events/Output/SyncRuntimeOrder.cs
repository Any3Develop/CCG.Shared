using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class SyncRuntimeOrder : GameEvent
    {
        public IRuntimeOrderModel RuntimeModel { get; set; }
    }
}