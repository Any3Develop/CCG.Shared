using CCG_Shared.Abstractions.Game.Runtime.Players;

namespace CCG_Shared.Game.Events.Context.Players
{
    public readonly struct BeforePlayerChangeEvent
    {
        public IRuntimePlayer RuntimePlayer { get; }

        public BeforePlayerChangeEvent(IRuntimePlayer runtimePlayer)
        {
            RuntimePlayer = runtimePlayer;
        }
    }
}