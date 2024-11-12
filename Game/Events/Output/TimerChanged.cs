using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class TimerChanged : GameEventBase
    {
        public IRuntimeTimerModel RuntimeModel { get; set; }
    }
}