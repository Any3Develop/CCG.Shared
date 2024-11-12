using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Timer
{
    public readonly struct TimerTurnChangedEvent
    {
        public IRuntimeTimer RuntimeTimer { get; }

        public TimerTurnChangedEvent(IRuntimeTimer runtimeTimer)
        {
            RuntimeTimer = runtimeTimer;
        }
    }
}