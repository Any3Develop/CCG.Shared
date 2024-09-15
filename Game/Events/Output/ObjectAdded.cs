using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Game.Events.Output
{
    public class AddedObject : GameEvent
    {
        public IRuntimeObjectData RuntimeData { get; set; }
    }
}