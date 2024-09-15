namespace CCG.Shared.Abstractions.Game.Runtime.Data
{
    public interface IRuntimeCardData : IRuntimeObjectData
    {
        int? Position { get; set; }
    }
}