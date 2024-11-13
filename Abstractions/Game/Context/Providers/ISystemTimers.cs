using CCG.Shared.Abstractions.Game.Context.EventSource;

namespace CCG.Shared.Abstractions.Game.Context.Providers
{
    public interface ISystemTimers : IDisposable
    {
        IEventsSource EventsSource { get; }
        IEventPublisher EventPublisher { get; }

        int? Current(string timerId);
        string Start(int durationMs);
        string Start(int fromMs, int toMs, int tickPeriodMs, bool tickNotify = true);
        
        bool Remove(string timerId);
        bool Pause(string timerId, bool value);
        bool Restart(string timerId);
    }
}