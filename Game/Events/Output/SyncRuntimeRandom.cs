using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class SyncRuntimeRandom : GameEvent
    {
        public IRuntimeRandomModel RuntimeModel { get; set; }
    }
}