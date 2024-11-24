using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Config
{
    public class EffectConfig : IConfig
    {
        public string Id { get; private set; }
        public int Value { get; private set; }
        public int Lifetime { get; private set; }
        public EffectLogicId EffectLogicId { get; private set; }
        public EffectVisualId EffectVisualId { get; private set; }
    }
}