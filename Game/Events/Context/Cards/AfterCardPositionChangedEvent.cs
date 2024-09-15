using CCG.Shared.Abstractions.Game.Runtime.Cards;

namespace CCG.Shared.Game.Events.Context.Cards
{
    public readonly struct AfterCardPositionChangedEvent
    {
        public IRuntimeCard RuntimeObject { get; }

        public AfterCardPositionChangedEvent(IRuntimeCard runtimeObject)
        {
            RuntimeObject = runtimeObject;
        }
    }
}