using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime.Heroes
{
    public class RuntimeHero : RuntimeHeroBase
    {
        protected override bool IsObjectAlive()
        {
            return RuntimeModel != null
                   && StatsCollection != null
                   && RuntimeModel.State != ObjectState.Destroyed
                   && StatsCollection.TryGet(StatType.Hp, out var statHp)
                   && statHp.RuntimeModel.Value > 0;
        }
    }
}