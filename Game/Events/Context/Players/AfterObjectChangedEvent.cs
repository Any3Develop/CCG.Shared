using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Players
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