using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeStat : IRuntimeObjectBase
    {
        new StatConfig Config { get; }
        new IRuntimeStatModel RuntimeModel { get; }
        
        IRuntimeStat Sync(IRuntimeStatModel runtimeModel);
        void SetValue(int value, bool notify = true);
        void SetMax(int value, bool notify = true);
        void Reset(bool notify = true);
    }
}