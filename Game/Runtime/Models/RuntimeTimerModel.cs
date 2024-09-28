using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeTimerModel : IRuntimeTimerModel
    {
        public int Id { get; set; }
        public string ConfigId { get; set; }
        public string OwnerId { get; set; }
        public int Turn { get; set; }
        public int Round { get; set; }
        public double TimeLeft { get; set; }
        public TimerState State { get; set; }
    }
}