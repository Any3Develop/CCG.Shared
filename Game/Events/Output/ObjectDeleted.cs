using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class ObjectDeleted : GameEvent
    {
        public IRuntimeObjectModel RuntimeModel { get; set; }
    }
}