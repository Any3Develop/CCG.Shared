using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IObjectEventProcessor
    {
        void Subscribe(IRuntimeObjectBase runtimeObject);
    }
}