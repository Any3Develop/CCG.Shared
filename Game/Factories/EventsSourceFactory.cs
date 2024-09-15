using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Game.Context.EventSource;

namespace CCG.Shared.Game.Factories
{
    public class EventsSourceFactory : IEventsSourceFactory
    {
        public IEventsSource Create(params object[] args)
        {
            return new EventsSource();
        }
    }
}