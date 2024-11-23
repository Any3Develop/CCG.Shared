namespace CCG.Shared.Game.Events.Context.Players
{
    public readonly struct AfterPlayerLostEvent
    {
        public string PlayerId { get; }

        public AfterPlayerLostEvent(string playerId)
        {
            PlayerId = playerId;
        }
    }
}