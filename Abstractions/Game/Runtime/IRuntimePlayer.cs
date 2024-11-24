using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimePlayer : IRuntimeBase
    {
        new PlayerConfig Config { get; }
        new IRuntimePlayerModel RuntimeModel { get; }
        IStatsCollection StatsCollection { get; }

        IRuntimePlayer Sync(IRuntimePlayerModel runtimeModel);
        bool TrySpendMana(int value);
        void SetReady(bool value);
    }
}