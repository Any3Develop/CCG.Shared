using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Collections
{
    public interface IStatsCollection : IRuntimeCollection<IRuntimeStat>
    {
        IRuntimeStat Get(StatType type);
    }
}