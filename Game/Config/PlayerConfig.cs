using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Game.Config
{
    public class PlayerConfig : IConfig
    {
        public string Id { get; set; }
        public string[] Stats { get; set; } = Array.Empty<string>();
    }
}