using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class StatChanged : GameEvent
    {
        public IRuntimeStatModel RuntimeModel { get; set; }
    }
}