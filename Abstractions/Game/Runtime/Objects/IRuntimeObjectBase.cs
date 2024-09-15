using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Abstractions.Game.Runtime.Objects
{
    public interface IRuntimeObjectBase : IDisposable
    {
        IRuntimeDataBase RuntimeData { get; }
        IEventsSource EventsSource { get; }
        IEventPublisher EventPublisher { get; }
    }
}