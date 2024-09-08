using CCG_Shared.Abstractions.Game.Runtime.Players;

namespace CCG_Shared.Game.Events.Context
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