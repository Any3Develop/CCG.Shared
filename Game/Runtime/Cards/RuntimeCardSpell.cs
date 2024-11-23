using CCG.Shared.Game.Enums;

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
    }
}