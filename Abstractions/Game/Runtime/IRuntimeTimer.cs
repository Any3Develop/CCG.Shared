using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeTimer
    {
        TimerConfig Config { get; }
        IRuntimeTimerModel RuntimeModel { get; }

        void Start();
        void End();
        
        IRuntimeTimer Sync(IRuntimeTimerModel runtimeModel);
        void SetState(TimerState value, bool notify = true);
        void RemoveState(TimerState value, bool notify = true);
        void SwitchState(TimerState value, bool notify = true);
        void SetOwner(string value, bool notify = true);
        void Pause(bool value, bool notify = true);
        void PassTurn(bool notify = true);
        void SetActionTime(int durationMs, bool inParallel = false);
        void RemoveEnding(bool notify = true);
    }
}