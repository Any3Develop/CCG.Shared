using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Timer
{
    public readonly struct TimerChangedEvent
    {
        public IRuntimeTimer RuntimeTimer { get; }

        public TimerChangedEvent(IRuntimeTimer runtimeTimer)
        {
            RuntimeTimer = runtimeTimer;
        }
    }
}