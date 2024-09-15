using CCG.Shared.Game.Data.Enums;

namespace CCG.Shared.Game.Events.Output
{
    public class ObjectStateChanged : GameEvent
    {
        public int Id { get; set; }
        public ObjectState Previous { get; set; }
        public ObjectState State { get; set; }
    }
}