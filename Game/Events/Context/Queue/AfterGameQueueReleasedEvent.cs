using CCG.Shared.Abstractions.Game.Events;

namespace CCG.Shared.Game.Events.Context.Queue
{
    public class AfterGameQueueReleasedEvent
    {
        public List<IGameEvent> Queue { get; }

        public AfterGameQueueReleasedEvent(List<IGameEvent> queue)
        {
            Queue = queue;
        }
    }
}