using CCG_Shared.Abstractions.Game.Events;

namespace CCG_Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IGameEventProcessor
    {
        void Process(IGameEvent gameEvent);
    }
}