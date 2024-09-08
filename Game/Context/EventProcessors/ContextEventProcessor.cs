using CCG_Shared.Abstractions.Game.Context;
using CCG_Shared.Abstractions.Game.Context.EventProcessors;
using CCG_Shared.Game.Events.Context.Queue;
using CCG_Shared.Game.Events.Output;
using CCG_Shared.Game.Utils;

namespace CCG_Shared.Game.Context.EventProcessors
{
    public class ContextEventProcessor : IContextEventProcessor
    {
        private readonly IContext context;
        private bool initialized;

        public ContextEventProcessor(IContext context)
        {
            this.context = context;
        }

        public virtual void Subscribe()
        {
            if (initialized)
                return;

            initialized = true;
            OnSubscribed();
        }

        protected virtual void OnSubscribed()
        {
            context.EventSource.Subscribe<AfterGameQueueReleasedEvent>(data =>
            {
                var syncOrderEvent = new SyncRuntimeOrder();
                var syncIdEvent = new SyncRuntimeId();
                syncOrderEvent.Order = context.RuntimeOrderProvider.Next();
                syncIdEvent.Order = context.RuntimeOrderProvider.Next();
                syncOrderEvent.RuntimeData = context.RuntimeOrderProvider.RuntimeData.Clone();
                syncIdEvent.RuntimeData = context.RuntimeIdProvider.RuntimeData.Clone();
                
                data.Queue.Add(syncOrderEvent);
                data.Queue.Add(syncIdEvent);
            }, order: int.MinValue);
        }
    }
}