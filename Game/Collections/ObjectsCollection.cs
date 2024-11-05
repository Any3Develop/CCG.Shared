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
        
        protected override int GetId(IRuntimeObject value) =>
            value?.RuntimeModel?.Id ?? int.MinValue;

        public int GetOccupiedTableSpace(string ownerId) // TODO: move to conditions
        {
            var checkOwner = !string.IsNullOrWhiteSpace(ownerId); 
            return GetAll<IRuntimeCard>().Count(x => (!checkOwner || x.RuntimeModel.OwnerId == ownerId) 
                                                     && x.RuntimeModel.State == ObjectState.InTable);
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