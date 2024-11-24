using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Events;
using CCG.Shared.Game.Events.Context.Commands;
using CCG.Shared.Game.Events.Context.Queue;

namespace CCG.Shared.Game.Context.Processors
{
    public class GameQueueCollector : IGameQueueCollector
    {
        private readonly IContext context;
        private readonly Queue<IGameEvent> queue;
        private IDisposable commandExecutionListener;
        private string predictionId;

        public GameQueueCollector(IContext context)
        {
            this.context = context;
            queue = new Queue<IGameEvent>();
        }

        public void Start()
        {
            commandExecutionListener = context.EventSource.Subscribe<BeforeCommandExecuteEvent>(SetupPredictions);
        }

        public void End()
        {
            commandExecutionListener?.Dispose();
            commandExecutionListener = null;
            queue.Clear();
        }

        public void Register(IGameEvent value)
        {
            value.Order = context.RuntimeOrderProvider.Next();
            value.PredictionId = predictionId;
            queue.Enqueue(value);
        }

        public void Release()
        {
            var releaseEvent = new AfterGameQueueReleasedEvent(queue.ToList());
            predictionId = null;
            queue.Clear();
            context.EventPublisher.Publish(releaseEvent);
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