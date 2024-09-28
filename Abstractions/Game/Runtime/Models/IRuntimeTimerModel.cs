using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeTimerModel : IRuntimeModelBase
    {
        int Turn { get; set; }
        int Round { get; set; }
        double TimeLeft { get; set; }
        TimerState State { get; set; }
    }
}