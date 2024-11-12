namespace CCG.Shared.Game.Events.Context.SystemTimers
{
    public readonly struct SystemTimerStartedEvent
    {
        public string TimerId { get; }

        public SystemTimerStartedEvent(string timerId)
        {
            TimerId = timerId;
        }
    }
}