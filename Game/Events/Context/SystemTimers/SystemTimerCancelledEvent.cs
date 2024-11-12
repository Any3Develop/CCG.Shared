namespace CCG.Shared.Game.Events.Context.SystemTimers
{
    public readonly struct SystemTimerCancelledEvent
    {
        public string TimerId { get; }

        public SystemTimerCancelledEvent(string timerId)
        {
            TimerId = timerId;
        }
    }
}