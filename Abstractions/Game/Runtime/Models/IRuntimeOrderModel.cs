namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeOrderModel : IContextModel
    {
        int NextOrder { get; set; }
    }
}