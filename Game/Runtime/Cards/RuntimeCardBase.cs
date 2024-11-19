using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Events.Context.Cards;

namespace CCG.Shared.Game.Runtime.Cards
{
    public abstract class RuntimeCardBase : RuntimeObjectBase, IRuntimeCard
    {
        public new CardConfig Config => (CardConfig) base.Config;
        public new IRuntimeCardModel RuntimeModel => (IRuntimeCardModel) base.RuntimeModel;

        // TODO: some special base functions
        
        public void SetPosition(int? value, bool notify = true)
        {
            if (notify)
                EventPublisher.Publish(new BeforeCardPositionChangeEvent(this));
            
            RuntimeModel.Position = value;
            
            if (notify)
                EventPublisher.Publish(new AfterCardPositionChangedEvent(this));
        }
    }
}