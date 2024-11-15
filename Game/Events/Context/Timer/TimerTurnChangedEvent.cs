namespace CCG.Shared.Game.Events.Context.Timer
{
    public readonly struct TimerTurnChangedEvent
    {
        public string OwnerId { get; }
        public string PrevOwnerId { get; }

        public TimerTurnChangedEvent(string ownerId, string prevOwnerId)
        {
            OwnerId = ownerId;
            PrevOwnerId = prevOwnerId;
        }
    }
}