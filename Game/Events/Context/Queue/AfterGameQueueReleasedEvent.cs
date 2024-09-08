using CCG_Shared.Abstractions.Game.Events;

namespace CCG_Shared.Game.Events.Context.Queue
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