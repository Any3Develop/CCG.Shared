using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Events.Output
{
    public class ObjectStateChanged : GameEventBase
    {
        public int Id { get; set; }
        public ObjectState Previous { get; set; }
        public ObjectState State { get; set; }
    }
}