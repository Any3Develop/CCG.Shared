using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Abstractions.Game.Runtime.Stats;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeStatFactory : IRuntimeFactory<IRuntimeStat, IRuntimeStatData> {}
}