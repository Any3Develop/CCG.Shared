using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeObjectFactory
    {
        IRuntimeObjectModel CreateModel(string ownerId, string dataId);
        IRuntimeObject Create(IRuntimeObjectModel runtimeModel, bool notify = false);
        void Restore(IEnumerable<IRuntimeObjectModel> runtimeModels);
    }
}