using CCG.Shared.Abstractions.Game.Events;

namespace CCG.Shared.Abstractions.Game.Context.Processors
{
    public interface IGameEventProcessor
    {
        void Process(IGameEvent gameEvent);
    }
}