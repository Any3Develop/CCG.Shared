using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeTimerModel : IRuntimeTimerModel
    {
        public string OwnerId { get; set; }
        public int Turn { get; set; }
        public int Round { get; set; }
        public double TimeLeft { get; set; }
        public DateTime? Paused { get; set; } // TODO
        public DateTime? Ended { get; set; } // TODO
        public TimerState State { get; set; }
    }
}