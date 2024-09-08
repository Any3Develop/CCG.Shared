using CCG_Shared.Abstractions.Game.Runtime.Stats;

namespace CCG_Shared.Game.Events.Context.Stats
{
    public readonly struct AfterStatDeletedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AfterStatDeletedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}