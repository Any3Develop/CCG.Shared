using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class StatAdded : GameEvent
    {
        public IRuntimeStatModel RuntimeModel { get; set; }
    }
}