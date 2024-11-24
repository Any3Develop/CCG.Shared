using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Args;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeEffectFactory
    {
        public IRuntimeEffectModel CreateModel(
            int runtimeOwnerId,
            string playerOwnerId,
            string configId,
            IEnumerable<int> targets,
            params IRuntimeEffectArg[] args);

        public IRuntimeEffect Create(IRuntimeEffectModel runtimeModel, bool notify = true);
    }
}