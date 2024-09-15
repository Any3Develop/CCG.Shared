namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeStatModel : IRuntimeModelBase
    {
        int RuntimeOwnerId { get; set; }
        int Max { get; set; }
        int Value { get; set; }
    }
}