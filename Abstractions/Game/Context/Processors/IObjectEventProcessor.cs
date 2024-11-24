using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Abstractions.Game.Context.Processors
{
    public interface IObjectEventProcessor
    {
        void Subscribe(IRuntimeBase runtimeObject);
    }
}