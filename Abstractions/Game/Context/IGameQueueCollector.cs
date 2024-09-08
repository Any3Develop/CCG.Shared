using CCG_Shared.Abstractions.Game.Events;

namespace CCG_Shared.Abstractions.Game.Context
{
    public interface IGameQueueCollector : IDisposable
    {
        void Register(IGameEvent value);
        void Release();
    }
}