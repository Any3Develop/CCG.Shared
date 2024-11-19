using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface ISessionFactory
    {
        ISession Create(string sessionId, List<SessionPlayer> players);
        ISession Restore(IContextModel[] models);
    }
}