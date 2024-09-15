using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Game.Config.Enums;

namespace CCG.Shared.Game.Config
{
    public class EffectConfig : IConfig
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
        public LogicId LogicId { get; set; }
        public VisualId VisualId { get; set; }
    }
}