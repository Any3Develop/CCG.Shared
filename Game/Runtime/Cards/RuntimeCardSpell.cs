using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime.Args;

namespace CCG.Shared.Game.Runtime.Cards
{
    public class RuntimeCardSpell : RuntimeCardBase
    {
        protected override bool IsObjectAlive()
        {
            return RuntimeModel != null
                   && StatsCollection != null
                   && RuntimeModel.State != ObjectState.Graveyard 
                   && RuntimeModel.State != ObjectState.Destroyed;
        }

        protected override bool OnReceiveDamage(ref HitArgs hit) => false;
        protected override bool TryCounterAttack(ref HitArgs hit) => false;
    }
}