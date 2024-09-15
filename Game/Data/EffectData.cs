using CCG.Shared.Abstractions.Game.Data;
using CCG.Shared.Game.Data.Enums;

namespace CCG.Shared.Game.Data
{
    public class EffectData : IData
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
        public LogicId LogicId { get; set; }
        public VisualId VisualId { get; set; }
    }
}