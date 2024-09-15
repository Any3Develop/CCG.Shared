using CCG.Shared.Abstractions.Game.Runtime.Players;

namespace CCG.Shared.Game.Events.Context
{
    public readonly struct PlayerInitializeEvent
    {
        public IRuntimePlayer RuntimePlayer { get; }

        public PlayerInitializeEvent(IRuntimePlayer runtimePlayer)
        {
            RuntimePlayer = runtimePlayer;
        }
    }
}