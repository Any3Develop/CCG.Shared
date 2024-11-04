namespace CCG.Shared.Abstractions.Game.Context.EventSource
{
    public interface IEventsSourceFactory
    {
        IEventsSource CreateSource(params object[] args);
        IEventPublisher CreatePublisher(params object[] args);
    }
}