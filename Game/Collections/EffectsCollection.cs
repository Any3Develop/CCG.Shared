using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Events.Context.Effects;

namespace CCG.Shared.Game.Collections
{
    public class EffectsCollection : RuntimeCollectionBase<IRuntimeEffect>, IEffectsCollection
    {
        private readonly IEventPublisher eventPublisher;

        public EffectsCollection(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
        
        public override void AddNotify(IRuntimeEffect value)
        {
            eventPublisher.Publish(new AfterEffectAddedEvent(value));
        }

        public override void RemoveNotify(IRuntimeEffect value)
        {
            eventPublisher.Publish(new AfterEffectDeletedEvent(value));
        }
    }
}