namespace CCG.Shared.Abstractions.Game.Context.EventSource
{
    public interface IEventPublisher
    {
        void Publish(object value);
        Task PublishAsync(object value);
        Task PublishParallelAsync(object value);
    }
}