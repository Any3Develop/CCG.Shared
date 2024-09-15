using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeObjectBase : IDisposable
    {
        IConfig Config { get; }
        IRuntimeModelBase RuntimeModel { get; }
        
        IEventsSource EventsSource { get; }
        IEventPublisher EventPublisher { get; }
    }
}