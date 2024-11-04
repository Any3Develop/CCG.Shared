namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimePlayerModel : IContextModel
    {
        int Id { get; }
        string Name { get; }
        string ConfigId { get; }
        string OwnerId { get; }
        bool Ready { get; set; }
        List<IRuntimeStatModel> Stats { get; }
    }
}