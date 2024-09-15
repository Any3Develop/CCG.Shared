using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Stats
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