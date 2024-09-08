using CCG_Shared.Abstractions.Game.Context.EventSource;
using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Abstractions.Game.Runtime.Objects
{
    public interface IRuntimeObjectBase : IDisposable
    {
        IRuntimeDataBase RuntimeData { get; }
        IEventsSource EventsSource { get; }
        IEventPublisher EventPublisher { get; }
    }
}