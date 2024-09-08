using CCG_Shared.Abstractions.Game.Runtime.Stats;

namespace CCG_Shared.Game.Events.Context.Stats
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