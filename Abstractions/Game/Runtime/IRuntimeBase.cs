using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeBase : IDisposable
    {
        IConfig Config { get; }
        IRuntimeBaseModel RuntimeModel { get; }
        
        IEventsSource EventsSource { get; }
        IEventPublisher EventPublisher { get; }
    }
}