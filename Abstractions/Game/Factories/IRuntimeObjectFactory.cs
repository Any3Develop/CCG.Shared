using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Abstractions.Game.Runtime.Objects;

namespace CCG_Shared.Abstractions.Game.Factories
{
    public interface IRuntimeObjectFactory : IRuntimeFactory<IRuntimeObject, IRuntimeObjectData>{}
}