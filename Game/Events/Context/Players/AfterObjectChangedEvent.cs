using CCG_Shared.Abstractions.Game.Runtime.Players;

namespace CCG_Shared.Game.Events.Context.Players
{
    public readonly struct AfterPlayerChangedEvent
    {
        public IRuntimePlayer RuntimePlayer { get; }

        public AfterPlayerChangedEvent(IRuntimePlayer runtimePlayer)
        {
            RuntimePlayer = runtimePlayer;
        }
    }
}