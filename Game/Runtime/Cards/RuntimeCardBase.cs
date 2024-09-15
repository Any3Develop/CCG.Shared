using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Events.Context.Cards;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Runtime.Cards
{
    public abstract class RuntimeCardBase : RuntimeObjectBase, IRuntimeCard
    {
        public new CardConfig Config => (CardConfig) base.Config;
        public new IRuntimeCardModel RuntimeModel => (IRuntimeCardModel) base.RuntimeModel;

        public void SetPosition(int? value, bool notify = true)
        {
            if (!Initialized)
                return;
            
            EventPublisher.Publish<BeforeCardPositionChangeEvent>(notify, this);
            RuntimeModel.Position = value;
            EventPublisher.Publish<AfterCardPositionChangedEvent>(notify, this);
        }
    }
}