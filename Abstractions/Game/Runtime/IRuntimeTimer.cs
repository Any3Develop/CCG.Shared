using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeTimer : IDisposable
    {
        new TimerConfig Config { get; }
        new IRuntimeTimerModel RuntimeModel { get; }
        
        IRuntimeTimer Sync(IRuntimeTimerModel runtimeModel);
        void SetState(TimerState value, bool notify = true);
        void SetTurnOwner(string value, bool notify = true);
        void PassTurn(bool notify = true);
    }
}