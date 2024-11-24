using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Events.Context.Queue;
using CCG.Shared.Game.Events.Output;

namespace CCG.Shared.Game.Context.Processors
{
    public class ContextEventProcessor : IContextEventProcessor
    {
        private readonly IContext context;
        private IDisposable releaseQueueListener;

        public ContextEventProcessor(IContext context)
        {
            this.context = context;
        }

        public virtual void Start()
        {
            if (releaseQueueListener != null)
                return;

            var orderProvider = context.RuntimeOrderProvider;
            var idProvider = context.RuntimeIdProvider;
            var randomProvider = context.RuntimeRandomProvider;
            
            releaseQueueListener = context.EventSource.Subscribe<AfterGameQueueReleasedEvent>(data =>
            {
                var syncRandomEvent = new SyncRuntimeRandom();
                var syncOrderEvent = new SyncRuntimeOrder();
                var syncIdEvent = new SyncRuntimeId();

                syncRandomEvent.Order = orderProvider.Next();
                syncOrderEvent.Order = orderProvider.Next();
                syncIdEvent.Order = orderProvider.Next();

                syncOrderEvent.RuntimeModel = orderProvider.RuntimeModel.DeepCopy();
                syncIdEvent.RuntimeModel = idProvider.RuntimeModel.DeepCopy();
                syncRandomEvent.RuntimeModel = randomProvider.RuntimeModel.DeepCopy();

                data.Queue.Add(syncRandomEvent);
                data.Queue.Add(syncOrderEvent);
                data.Queue.Add(syncIdEvent);
            }, order: int.MinValue);
        }

        public virtual void End()
        {
            releaseQueueListener?.Dispose();
            releaseQueueListener = null;
        }
    }
}