using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Events.Output
{
    public class ObjectHit : GameEventBase
    {
        public int TargetId { get; set; }
        public int AttackerId { get; set; }
        public int Damage { get; set; }
        public DamageType Type { get; set; }
    }
}