﻿using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Events.Context.Effects;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Collections
{
    public class EffectsCollection : RuntimeCollectionBase<IRuntimeEffect>, IEffectsCollection
    {
        private readonly IEventPublisher eventPublisher;

        public EffectsCollection(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
        
        protected override int GetId(IRuntimeEffect value) =>
            value?.RuntimeModel?.Id ?? int.MinValue;
        
        public override bool Add(IRuntimeEffect value, bool notify = true)
        {
            var result = base.Add(value, notify);
            eventPublisher.Publish<AfterEffectAddedEvent>(notify && result, value);
            return result;
        }

        public override bool Remove(int id, bool notify = true)
        {
            if (!TryGet(id, out var value))
                return false;

            var result = base.Remove(value, notify);
            eventPublisher.Publish<AfterEffectDeletedEvent>(notify && result, value);
            return result;
        }
    }
}