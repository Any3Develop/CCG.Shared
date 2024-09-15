using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class ObjectDeleted : GameEventBase
    {
        public IRuntimeObjectModel RuntimeModel { get; set; }
    }
}