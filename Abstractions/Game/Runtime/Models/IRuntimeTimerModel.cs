using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeTimerModel : IContextModel
    {
        string OwnerId { get; }
        int Turn { get; set; }
        int Round { get; set; }
        double TimeLeft { get; set; }
        TimerState State { get; set; }
    }
}