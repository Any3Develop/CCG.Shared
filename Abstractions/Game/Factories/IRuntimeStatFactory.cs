using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Abstractions.Game.Runtime.Stats;

namespace CCG_Shared.Abstractions.Game.Factories
{
    public interface IRuntimeStatFactory : IRuntimeFactory<IRuntimeStat, IRuntimeStatData> {}
}