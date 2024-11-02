namespace CCG.Shared.Abstractions.Game.Context.EventProcessors
{
    public interface IContextEventProcessor
    {
        void Start();
        void End();
    }
}