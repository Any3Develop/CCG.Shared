using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Abstractions.Game.Runtime.Objects;

namespace CCG.Shared.Abstractions.Game.Runtime.Players
{
    public interface IRuntimePlayer : IRuntimeObjectBase
    {
        new IRuntimePlayerData RuntimeData { get; }
        IStatsCollection StatsCollection { get; }

        IRuntimePlayer Sync(IRuntimePlayerData runtimeData, bool notify = true);
        bool TrySpendMana(int value);
        void SetReady(bool value);
    }
}