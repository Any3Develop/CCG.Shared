using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Events.Context.Players
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