using CCG_Shared.Abstractions.Game.Runtime.Cards;

namespace CCG_Shared.Game.Events.Context.Cards
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