namespace CCG.Shared.Game.Events.Context.SystemTimers
{
    public readonly struct SystemTimerEndedEvent
    {
        public string TimerId { get; }

        public SystemTimerEndedEvent(string timerId)
        {
            TimerId = timerId;
        }
    }
}