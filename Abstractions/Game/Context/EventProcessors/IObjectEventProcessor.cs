using CCG_Shared.Abstractions.Game.Runtime.Objects;

namespace CCG_Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IObjectEventProcessor
    {
        void Subscribe(IRuntimeObjectBase runtimeObject);
    }
}