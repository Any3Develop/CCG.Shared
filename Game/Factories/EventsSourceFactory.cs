using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Game.Context.EventSource;

namespace CCG.Shared.Game.Factories
{
    public class EventsSourceFactory : IEventsSourceFactory
    {
        public IEventsSource CreateSource(params object[] args)
        {
            return new EventsSource();
        }

        public IEventPublisher CreatePublisher(params object[] args)
        {
            return args.OfType<IEventPublisher>().FirstOrDefault();
        }
    }
}