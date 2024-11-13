using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface IContextInitializer
    {
        IContext Init(IContext context, string id, List<SessionPlayer> players);
        IContext Init(IContext context, IReadOnlyCollection<IContextModel> models);
    }
}