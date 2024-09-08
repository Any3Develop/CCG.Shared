using CCG_Shared.Abstractions.Game.Runtime.Cards;
using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Game.Data;
using CCG_Shared.Game.Events.Context.Cards;
using CCG_Shared.Game.Runtime.Objects;
using CCG_Shared.Game.Utils;

namespace CCG_Shared.Game.Runtime.Cards
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