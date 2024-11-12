namespace CCG.Shared.Game.Events.Context.SystemTimers
{
    public readonly struct SystemTimerPauseEvent
    {
        public string TimerId { get; }
        public bool Paused { get; }

        public SystemTimerPauseEvent(string timerId, bool paused)
        {
            TimerId = timerId;
            Paused = paused;
        }
    }
}