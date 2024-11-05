using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeEffect : IRuntimeObjectBase
    {
        new EffectConfig Config { get; }
        new IRuntimeEffectModel RuntimeModel { get; }

        IRuntimeEffect Sync(IRuntimeEffectModel runtimeModel);
        bool IsExecuteAllowed();
        void Execute();
        void Expire();
    }
}