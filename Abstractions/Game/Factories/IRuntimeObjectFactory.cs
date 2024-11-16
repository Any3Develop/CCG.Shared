using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeObjectFactory
    {
        IRuntimeObjectModel CreateModel(int? runtimeId, string ownerId, string dataId);
        IRuntimeObject Create(IRuntimeObjectModel runtimeData, bool notify = true);
    }
}