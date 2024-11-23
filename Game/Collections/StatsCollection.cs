using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Stats;

namespace CCG.Shared.Game.Collections
{
    public class StatsCollection : RuntimeCollectionBase<IRuntimeStat>, IStatsCollection
    {
        private readonly IEventPublisher eventPublisher;
        public StatsCollection(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
        
        public override void AddNotify(IRuntimeStat value)
        {
            eventPublisher.Publish(new AfterStatAddedEvent(value));
        }

        public override void RemoveNotify(IRuntimeStat value)
        {
            eventPublisher.Publish(new AfterStatDeletedEvent(value));
        }

        public bool TryGet(StatType type, out IRuntimeStat result)
        {
            result = GetFirst(x => x.Config.Type == type);
            return result != null;
        }

        public IRuntimeStat Get(StatType type)
        {
            return GetFirst(x => x.Config.Type == type);
        }

        public bool Contains(StatType type)
        {
            return GetFirst(x => x.Config.Type == type)?.RuntimeModel != null;
        }
    }
}