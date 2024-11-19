using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface IContextInitializer
    {
        IContext Init(IContext context, string sessionId, List<SessionPlayer> players);
        IContext Restore(IContext context, IContextModel[] models);
    }
}