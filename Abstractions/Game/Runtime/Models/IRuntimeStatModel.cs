using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeStatModel : IRuntimeModelBase
    {
        StatType Type { get; set; }
        int RuntimeOwnerId { get; set; }
        int Max { get; set; }
        int Value { get; set; }
    }
}