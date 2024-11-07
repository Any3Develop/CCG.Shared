using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeTimerModel : IContextModel
    {
        string OwnerId { get; set; }
        int Turn { get; set; }
        int Round { get; set; }
        double TimeLeft { get; set; }
        public DateTime? Paused { get; set; } // TODO
        public DateTime? Ended { get; set; } // TODO
        TimerState State { get; set; }
    }
}