namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimePlayerModel : IRuntimeModelBase
    {
        bool Ready { get; set; }
        List<IRuntimeStatModel> Stats { get; }
        
    }
}