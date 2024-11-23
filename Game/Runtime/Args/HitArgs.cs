using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime.Args
{
    public readonly struct HitArgs
    {
        public IRuntimeObject Attacker { get; }
        public IRuntimeObject Target { get; }
        public int Damage { get; }
        public DamageType Type { get; }

        public HitArgs(IRuntimeObject attacker, IRuntimeObject target, int damage, DamageType type)
        {
            Attacker = attacker;
            Target = target;
            Damage = damage;
            Type = type;
        }
    }
}