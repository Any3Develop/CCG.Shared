using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Events.Context.Objects;

namespace CCG.Shared.Game.Collections
{
    public class ObjectsCollection : RuntimeCollectionBase<IRuntimeObject>, IObjectsCollection
    {
        private readonly IEventPublisher eventPublisher;

        public ObjectsCollection(IEventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        public IEnumerable<TRuntime> GetAll<TRuntime>(
            ObjectState? state = null, 
            string ownerId = null, 
            bool asQuery = false, 
            params int[] runtimeIds) 
            where TRuntime : IRuntimeObject
        {
            var hasState = state.HasValue;
            var hasOwner = ownerId != null;
            var hasIds = runtimeIds.Length > 0;
            var query = base.GetAll<TRuntime>().Where(x =>
                (!hasState || x.RuntimeModel.State == state)
                && (!hasOwner || x.RuntimeModel.OwnerId == ownerId)
                && (!hasIds || runtimeIds.Contains(x.RuntimeModel.Id)));

            return asQuery ? query : query.ToArray();
        }

        public int GetOccupiedTableSpace(string ownerId) // TODO: move to conditions
        {
            var checkOwner = !string.IsNullOrWhiteSpace(ownerId); 
            return GetAll<IRuntimeCard>().Count(x => (!checkOwner || x.RuntimeModel.OwnerId == ownerId) 
                                                     && x.RuntimeModel.State == ObjectState.Table);
        }

        public override void AddNotify(IRuntimeObject value)
        {
            eventPublisher.Publish(new AfterObjectAddedEvent(value));
        }

        public override void RemoveNotify(IRuntimeObject value)
        {
            eventPublisher.Publish(new AfterObjectDeletedEvent(value));
        }
    }
}