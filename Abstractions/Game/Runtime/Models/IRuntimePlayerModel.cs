namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimePlayerModel : IContextModel
    {
        int Id { get; }
        string ConfigId { get; set; }
        string OwnerId { get; }
        bool Ready { get; set; }
        List<IRuntimeStatModel> Stats { get; }

    }
}