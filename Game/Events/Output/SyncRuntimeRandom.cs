using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Game.Events.Output
{
    public class SyncRuntimeRandom : GameEvent
    {
        public IRuntimeRandomData RuntimeData { get; set; }
    }
}