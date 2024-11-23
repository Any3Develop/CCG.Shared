using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Events;
using CCG.Shared.Game.Events.Context.Commands;
using CCG.Shared.Game.Events.Context.Queue;

namespace CCG.Shared.Game.Context.Processors
{
    public class GameQueueCollector : IGameQueueCollector
    {
        private readonly Queue<IGameEvent> queue;
        private readonly IEventsSource eventsSource;
        private readonly IEventPublisher eventPublisher;
        private readonly IRuntimeOrderProvider orderProvider;
        private IDisposable commandExecutionListener;
        private string predictionId;

        public GameQueueCollector(
            IEventsSource eventsSource,
            IEventPublisher eventPublisher,
            IRuntimeOrderProvider orderProvider)
        {
            this.eventsSource = eventsSource;
            this.eventPublisher = eventPublisher;
            this.orderProvider = orderProvider;
            queue = new Queue<IGameEvent>();
        }

        public void Start()
        {
            commandExecutionListener = eventsSource.Subscribe<BeforeCommandExecuteEvent>(SetupPredictions);
        }

        public void End()
        {
            commandExecutionListener?.Dispose();
            commandExecutionListener = null;
            queue.Clear();
        }

        public void Register(IGameEvent value)
        {
            value.Order = orderProvider.Next();
            value.PredictionId = predictionId;
            queue.Enqueue(value);
        }

        public void Release()
        {
            var releaseEvent = new AfterGameQueueReleasedEvent(queue.ToList());
            predictionId = null;
            queue.Clear();
            eventPublisher.Publish(releaseEvent);
        }

        private void SetupPredictions(BeforeCommandExecuteEvent eventData)
        {
            var cmdModel = eventData.Command.Model;
            if (cmdModel.IsNested)
                return;
            
            predictionId = eventData.Command.Model.PredictionId;
        }
    }
}