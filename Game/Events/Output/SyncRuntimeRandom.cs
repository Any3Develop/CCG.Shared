using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Game.Events.Output
{
    public class SyncRuntimeRandom : GameEvent
    {
        public IRuntimeRandomData RuntimeData { get; set; }
    }
}