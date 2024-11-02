using CCG.Shared.Abstractions.Game.Events;

namespace CCG.Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IGameQueueCollector
    {
        void Start();
        void End();
        
        void Register(IGameEvent value);
        void Release();
    }
}