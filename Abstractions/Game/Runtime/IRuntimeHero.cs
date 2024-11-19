using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeHero : IRuntimeObject
    {
        new HeroConfig Config { get; }
        new IRuntimeHeroModel RuntimeModel { get; }
    }
}