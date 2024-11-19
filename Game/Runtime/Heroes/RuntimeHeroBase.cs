using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Game.Runtime.Heroes
{
    public abstract class RuntimeHeroBase : RuntimeObjectBase, IRuntimeHero
    {
        public new HeroConfig Config => (HeroConfig) base.Config;
        public new IRuntimeHeroModel RuntimeModel => (IRuntimeHeroModel) base.RuntimeModel;
        
        // TODO: some special base functions
    }
}