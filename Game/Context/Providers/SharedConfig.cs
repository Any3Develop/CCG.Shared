using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Api.Lobby;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Game.Context.Providers
{
    public class SharedConfig : ISharedConfig
    {
        public TableConfig Table { get; set; }
        
        public DeckConfig Deck { get; set; }

        public TimerConfig Timer { get; set; }
        public PlayerConfig[] Players { get; set; } = Array.Empty<PlayerConfig>();
    }
}