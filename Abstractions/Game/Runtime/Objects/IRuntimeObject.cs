using CCG_Shared.Abstractions.Game.Collections;
using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Game.Data;
using CCG_Shared.Game.Data.Enums;

namespace CCG_Shared.Abstractions.Game.Runtime.Objects
{
    public interface IRuntimeObject : IRuntimeObjectBase
    {
        ObjectData Data { get; }
        new IRuntimeObjectData RuntimeData { get; }
        IStatsCollection StatsCollection { get; }
        IEffectsCollection EffectsCollection { get; }

        IRuntimeObject Sync(IRuntimeObjectData runtimeData, bool notify = true);
        void SetState(ObjectState value, bool notify = true);
    }
}