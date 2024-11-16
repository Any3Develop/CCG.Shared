using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeEffectFactory
    {
        public IRuntimeEffectModel CreateModel(int? runtimeId, int runtimeOwnerId, string ownerId, string dataId);

        public IRuntimeEffect Create(IRuntimeEffectModel runtimeModel, bool notify = true);
    }
}