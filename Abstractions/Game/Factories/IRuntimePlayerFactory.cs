using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Abstractions.Game.Runtime.Players;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimePlayerFactory : IRuntimeFactory<IRuntimePlayer, IRuntimePlayerData>{}
}