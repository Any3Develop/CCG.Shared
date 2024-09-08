using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Abstractions.Game.Runtime.Objects;
using CCG_Shared.Game.Data;

namespace CCG_Shared.Abstractions.Game.Runtime.Effects
{
    public interface IRuntimeEffect : IRuntimeObjectBase
    {
        EffectData Data { get; }
        new IRuntimeEffectData RuntimeData { get; }

        IRuntimeEffect Sync(IRuntimeEffectData runtimeData, bool notify = true);
        bool IsExecuteAllowed();
        void Execute();
        void Expire();
    }
}