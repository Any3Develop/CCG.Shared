using CCG.Shared.Game.Config;

namespace CCG.Shared.Game.Context
{
    public class DatabaseModel
    {
        public ObjectConfig[] Objects { get; set; }
        public EffectConfig[] Effects { get; set; }
        public StatConfig[] Stats { get; set; }
        public PlayerConfig[] Players { get; set; }
    }
}