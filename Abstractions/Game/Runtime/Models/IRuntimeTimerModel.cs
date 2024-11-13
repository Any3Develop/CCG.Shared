using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Runtime.Models
{
    public interface IRuntimeTimerModel : IContextModel
    {
        string OwnerId { get; set; }
        int Turn { get; set; }
        int Round { get; set; }
        int TimeLeftMs { get; set; }
        public List<ActionTimestamp> Actions { get; set; }
        TimerState State { get; set; }
    }
}