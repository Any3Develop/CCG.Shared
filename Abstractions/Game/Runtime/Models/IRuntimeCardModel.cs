namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeCardModel : IRuntimeObjectModel
    {
        int? Position { get; set; }
    }
}