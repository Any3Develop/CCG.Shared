namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeIdModel : IContextModel
    {
        int NextId { get; set; }
    }
}