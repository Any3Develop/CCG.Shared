using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeObject : IRuntimeObjectBase
    {
        new ObjectConfig Config { get; }
        new IRuntimeObjectModel RuntimeModel { get; }
        IStatsCollection StatsCollection { get; }
        IEffectsCollection EffectsCollection { get; }

        IRuntimeObject Sync(IRuntimeObjectModel runtimeModel);
        void SetState(ObjectState value, ObjectState? previous = null, bool notify = true);
    }
}