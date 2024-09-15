using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Events;
using CCG.Shared.Game.Events.Context.Commands;
using CCG.Shared.Game.Events.Context.Queue;

namespace CCG.Shared.Game.Context
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
            commandExecutionListener = context.EventSource
                .Subscribe<BeforeCommandExecuteEvent>(ev => predictionId = ev.Command.Model.PredictionId);
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

        public void Dispose()
        {
            commandExecutionListener?.Dispose();
            commandExecutionListener = null;
            queue.Clear();
        }
    }
}