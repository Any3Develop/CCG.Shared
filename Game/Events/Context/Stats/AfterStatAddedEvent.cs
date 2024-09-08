using CCG_Shared.Abstractions.Game.Runtime.Stats;

namespace CCG_Shared.Game.Events.Context.Stats
{
    public readonly struct AfterStatAddedEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public AfterStatAddedEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}