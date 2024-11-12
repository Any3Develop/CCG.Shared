namespace CCG.Shared.Game.Events.Context.SystemTimers
{
    public readonly struct SystemTimerRestartedEvent
    {
        public string TimerId { get; }

        public SystemTimerRestartedEvent(string timerId)
        {
            TimerId = timerId;
        }
    }
}