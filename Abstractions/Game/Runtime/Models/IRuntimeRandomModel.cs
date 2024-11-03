namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeRandomModel : IContextModel
    {
        int Seed { get; set; }
        int Current { get; set; }
    }
}