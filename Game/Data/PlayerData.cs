using CCG.Shared.Abstractions.Game.Data;

namespace CCG.Shared.Game.Data
{
    public class PlayerData : IData
    {
        public string Id { get; set; }
        public string[] StatIds { get; set; } = Array.Empty<string>();
    }
}