using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Events.Context.Stats;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Collections
{
    public class StatsCollection : RuntimeCollectionBase<IRuntimeStat>, IStatsCollection
    {
        private readonly IEventPublisher eventPublisher;
        public StatsCollection(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        protected override int GetId(IRuntimeStat value) =>
            value?.RuntimeModel?.Id ?? int.MinValue;
        
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