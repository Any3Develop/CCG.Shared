using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime.Models
{
    public readonly struct ActionTimestamp
    {
        public int Timestamp { get; }
        public int Duration { get; }
        public bool InParallel { get; }
        public ActionTimestamp(int timestamp, int duration, bool inParallel)
        {
            Timestamp = timestamp;
            Duration = duration;
            InParallel = inParallel;
        }
    }
    
    public class RuntimeTimerModel : IRuntimeTimerModel
    {
        public string OwnerId { get; set; }
        public int Turn { get; set; }
        public int Round { get; set; }
        public int TimeLeftMs { get; set; }
        public List<ActionTimestamp> Actions { get; set; } = new();
        public bool Paused { get; set; }
        public TimerState State { get; set; }
    }
}