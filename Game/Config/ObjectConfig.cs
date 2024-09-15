using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Game.Config.Enums;

namespace CCG.Shared.Game.Config
{
    public abstract class ObjectConfig : IConfig
    {
        public string Id { get; set; }
        public ObjectType Type { get; set; }
        public string Title { get; set; }
        public string ArtId { get; set; }
        public string[] Stats { get; set; } = Array.Empty<string>();
        public string[] Effects { get; set; } = Array.Empty<string>();
    }
}