using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Abstractions.Game.Runtime.Objects;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeObjectFactory : IRuntimeFactory<IRuntimeObject, IRuntimeObjectData>{}
}