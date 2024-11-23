using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime.Args;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeObject : IRuntimeObjectBase
    {
        new ObjectConfig Config { get; }
        new IRuntimeObjectModel RuntimeModel { get; }
        bool IsAlive { get; }
        IStatsCollection StatsCollection { get; }
        IEffectsCollection EffectsCollection { get; }

        IRuntimeObject Sync(IRuntimeObjectModel runtimeModel);
        bool ReceiveHit(HitArgs hit);
        void SetState(ObjectState value, ObjectState? previous = null, bool notify = true);
        void Spawn(bool notify = true);
    }
}