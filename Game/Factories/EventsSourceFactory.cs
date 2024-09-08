using CCG_Shared.Abstractions.Game.Context.EventSource;
using CCG_Shared.Game.Context.EventSource;

namespace CCG_Shared.Game.Factories
{
    public class EventsSourceFactory : IEventsSourceFactory
    {
        public IEventsSource Create(params object[] args)
        {
            return new EventsSource();
        }
    }
}