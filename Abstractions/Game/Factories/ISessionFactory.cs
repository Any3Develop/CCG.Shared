using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface ISessionFactory
    {
        ISession Create(string id, List<SessionPlayer> players);
        ISession Create(IContextModel[] models);
    }
}