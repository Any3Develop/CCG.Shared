using CCG.Shared.Abstractions.Game.Runtime.Stats;

namespace CCG.Shared.Game.Events.Context.Stats
{
    public readonly struct BeforeStatChangeEvent
    {
        public IRuntimeStat RuntimeStat { get; }

        public BeforeStatChangeEvent(IRuntimeStat runtimeStat)
        {
            RuntimeStat = runtimeStat;
        }
    }
}