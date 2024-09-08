using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Abstractions.Game.Runtime.Effects;

namespace CCG_Shared.Abstractions.Game.Factories
{
    public interface IRuntimeEffectFactory : IRuntimeFactory<IRuntimeEffect, IRuntimeEffectData>{}
}