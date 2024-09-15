using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Stats
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