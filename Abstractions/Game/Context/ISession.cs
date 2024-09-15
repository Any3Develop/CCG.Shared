using CCG.Shared.Game.Data;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface ISession
    {
        SessionPlayer[] Players { get; }
        IContext Context { get; }
        string Id { get; }
        void Build(params object[] args);
    }
}