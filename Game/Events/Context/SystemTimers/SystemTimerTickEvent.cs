namespace CCG.Shared.Game.Events.Context.SystemTimers
{
    public readonly struct SystemTimerTickEvent
    {
        public string TimerId { get; }
        public int CurrentMs { get; }

        public SystemTimerTickEvent(string timerId, int currentMs)
        {
            TimerId = timerId;
            CurrentMs = currentMs;
        }
    }
}