using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Game.Runtime.Data
{
    public class RuntimeCardData : RuntimeObjectData, IRuntimeCardData
    {
        public int? Position { get; set; }
    }
}