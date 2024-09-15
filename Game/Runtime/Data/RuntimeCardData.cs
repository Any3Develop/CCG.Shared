using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Game.Runtime.Data
{
    public class RuntimeCardData : RuntimeObjectData, IRuntimeCardData
    {
        public int? Position { get; set; }
    }
}