using CCG.Shared.Abstractions.Game.Events;

namespace CCG.Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IGameEventProcessor
    {
        void Process(IGameEvent gameEvent);
    }
}