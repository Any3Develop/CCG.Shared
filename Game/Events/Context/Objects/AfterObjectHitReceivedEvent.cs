using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime.Args;

namespace CCG.Shared.Game.Events.Context.Objects
{
    public readonly struct AfterObjectHitReceivedEvent
    {
        public int AttackerId { get; }
        public int TargetId { get; }
        public int Damage { get; }
        public DamageType Type { get; }

        public AfterObjectHitReceivedEvent(int attackerId, int targetId, int damage, DamageType type)
        {
            AttackerId = attackerId;
            TargetId = targetId;
            Damage = damage;
            Type = type;
        }

        public AfterObjectHitReceivedEvent(HitArgs hit)
        {
            AttackerId = hit.Attacker.RuntimeModel.Id;
            TargetId = hit.Target.RuntimeModel.Id;
            Damage = hit.Damage;
            Type = hit.Type;
        }
    }
}