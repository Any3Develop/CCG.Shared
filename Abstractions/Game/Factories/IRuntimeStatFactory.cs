using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeStatFactory
    {
        IRuntimeStatModel CreateModel(int runtimeOwnerId, string ownerId, string dataId);
        IRuntimeStat Create(IRuntimeStatModel runtimeModel, bool notify = true);
    }
}