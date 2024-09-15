using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class SyncRuntimeId : GameEventBase
    {
        public IRuntimeIdModel RuntimeModel { get; set; }
    }
}