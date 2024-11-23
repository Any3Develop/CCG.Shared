using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Collections
{
    public interface IStatsCollection : IRuntimeCollection<IRuntimeStat>
    {
        bool TryGet(StatType type, out IRuntimeStat result);
        IRuntimeStat Get(StatType type);
        bool Contains(StatType type);
    }
}