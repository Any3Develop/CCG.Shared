using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class SyncRuntimeRandom : GameEventBase
    {
        public IRuntimeRandomModel RuntimeModel { get; set; }
    }
}