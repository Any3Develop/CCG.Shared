using CCG.Shared.Api.Lobby;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface ISharedConfig
    {
        TableConfig Table { get; }
        DeckConfig Deck { get; }
        TimerConfig Timer { get; }
        PlayerConfig[] Players { get; }
    }
}