using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Abstractions.Game.Runtime.Effects;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeEffectFactory : IRuntimeFactory<IRuntimeEffect, IRuntimeEffectData>{}
}