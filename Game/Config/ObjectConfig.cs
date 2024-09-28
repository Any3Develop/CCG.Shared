using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Config
{
    public abstract class ObjectConfig : IConfig
    {
        public string Id { get; private set; }
        public ObjectType Type { get; private set; }
        public string Title { get; private set; }
        public string ArtId { get; private set; }
        public string[] Stats { get; private set; } = Array.Empty<string>();
        public string[] Effects { get; private set; } = Array.Empty<string>();
    }
}