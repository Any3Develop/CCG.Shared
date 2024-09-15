namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeModelBase
    {
        int Id { get; }
        string ConfigId { get; }
        string OwnerId { get; }
    }
}