using CCG.Shared.Game.Config;

namespace CCG.Shared.Api.Game
{
    public class DatabaseModel
    {
        public ObjectConfig[] Objects { get; set; }
        public EffectConfig[] Effects { get; set; }
        public StatConfig[] Stats { get; set; }
    }
}