using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Abstractions.Game.Runtime.Players;

namespace CCG_Shared.Abstractions.Game.Factories
{
    public interface IRuntimePlayerFactory : IRuntimeFactory<IRuntimePlayer, IRuntimePlayerData>{}
}