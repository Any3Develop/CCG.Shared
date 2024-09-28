using CCG.Shared.Abstractions.Game.Events;

namespace CCG.Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IGameQueueCollector : IDisposable
    {
        void Register(IGameEvent value);
        void Release();
    }
}