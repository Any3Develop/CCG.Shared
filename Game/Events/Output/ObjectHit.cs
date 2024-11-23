namespace CCG.Shared.Game.Events.Output
{
    public class ObjectHit : GameEventBase
    {
        public int RuntimeObjectId { get; set; }
        public int Value { get; set; }
    }
}