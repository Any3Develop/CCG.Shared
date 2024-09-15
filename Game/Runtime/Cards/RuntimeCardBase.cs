using CCG.Shared.Abstractions.Game.Runtime.Cards;
using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Game.Data;
using CCG.Shared.Game.Events.Context.Cards;
using CCG.Shared.Game.Runtime.Objects;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Runtime.Cards
{
    public abstract class RuntimeCardBase : RuntimeObject, IRuntimeCard
    {
        public new CardData Data => (CardData) base.Data;
        public new IRuntimeCardData RuntimeData => (IRuntimeCardData) base.RuntimeData;

        public void SetPosition(int? value, bool notify = true)
        {
            if (!Initialized)
                return;
            
            EventPublisher.Publish<BeforeCardPositionChangeEvent>(notify, this);
            RuntimeData.Position = value;
            EventPublisher.Publish<AfterCardPositionChangedEvent>(notify, this);
        }
    }
}