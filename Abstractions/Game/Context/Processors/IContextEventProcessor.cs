namespace CCG.Shared.Abstractions.Game.Context.Processors
{
    public interface IContextEventProcessor
    {
        void Start();
        void End();
    }
}