namespace CCG.Shared.Abstractions.Game.Context
{
    public interface ISession
    {
        IContext Context { get; }
        string Id { get; }

        void Start();
        void Ready();
        void End();
    }
}