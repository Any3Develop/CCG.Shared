using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Game.Events.Output
{
    public class AddedObject : GameEvent
    {
        public IRuntimeObjectData RuntimeData { get; set; }
    }
}