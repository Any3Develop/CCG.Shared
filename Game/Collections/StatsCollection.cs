﻿using Shared.Abstractions.Game.Collections;
using Shared.Abstractions.Game.Context.EventSource;
using Shared.Abstractions.Game.Runtime.Stats;
using Shared.Game.Events.Context.Stats;
using Shared.Game.Utils;

namespace Shared.Game.Collections
{
    public class StatsCollection : RuntimeCollectionBase<IRuntimeStat>, IStatsCollection
    {
        private readonly IEventPublisher eventPublisher;
        public StatsCollection(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        protected override int GetId(IRuntimeStat value) =>
            value?.RuntimeData?.Id ?? int.MinValue;
        
        public override bool Add(IRuntimeStat value, bool notify = true)
        {
            var result = base.Add(value, notify);
            eventPublisher.Publish<AfterStatAddedEvent>(notify && result, value);
            return result;
        }

        public override bool Remove(int id, bool notify = true)
        {
            if (!TryGet(id, out var value))
                return false;
            
            var result = base.Remove(value, notify);
            eventPublisher.Publish<AfterStatDeletedEvent>(notify && result, value);
            return result;
        }
    }
}