using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeStat : IRuntimeBase
    {
        new StatConfig Config { get; }
        new IRuntimeStatModel RuntimeModel { get; }
        
        int Current { get; }
        int Max { get; }
        
        IRuntimeStat Sync(IRuntimeStatModel runtimeModel);
        void Override(int value, int max, bool notify = true);
        void RiseToMax(bool notify = true);
        void SetValue(int value, bool notify = true);
        void Add(int value, bool notify = true);
        void Subtract(int value, bool notify = true);
        void SetMax(int value, bool notify = true);
        void Reset(bool notify = true);
    }
}