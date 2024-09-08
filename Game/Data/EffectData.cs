using CCG_Shared.Abstractions.Game.Data;
using CCG_Shared.Game.Data.Enums;

namespace CCG_Shared.Game.Data
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