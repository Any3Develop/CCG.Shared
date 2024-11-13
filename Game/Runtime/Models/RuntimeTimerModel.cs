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
        /// <summary>
        /// Current turn owner / player id.
        /// </summary>
        public string OwnerId { get; set; }
        /// <summary>
        /// Uniq player turn's number
        /// </summary>
        public int Turn { get; set; }
        /// <summary>
        /// One round is when all players have played their turn.
        /// </summary>
        public int Round { get; set; }
        /// <summary>
        /// Milliseconds countdown to zero, then timer will switch the turn.
        /// </summary>
        public int TimeLeftMs { get; set; }
        /// <summary>
        /// Recorded turn actions with their times are used to calculate when players should wait for the actions end.
        /// </summary>
        public List<ActionTimestamp> Actions { get; set; } = new();
        public TimerState State { get; set; }
    }
}