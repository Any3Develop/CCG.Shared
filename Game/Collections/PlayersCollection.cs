using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Game.Collections
{
    public class PlayersCollection : RuntimeCollectionBase<IRuntimePlayer>, IPlayersCollection
    {
        public IRuntimePlayer Get(string ownerId)
        {
            return GetFirst(x => x.RuntimeModel.OwnerId == ownerId);
        }

        public IRuntimePlayer GetOpposite(string ownerId)
        {
            return GetFirst(x => x.RuntimeModel.OwnerId != ownerId);
        }

        public IRuntimePlayer GetOpposite(IRuntimePlayer runtimePlayer)
        {
            return runtimePlayer?.RuntimeModel != null 
                ? GetFirst(x => x.RuntimeModel.OwnerId != runtimePlayer.RuntimeModel.OwnerId) 
                : default;
        }

        public bool TryGet(string ownerId, out IRuntimePlayer result)
        {
            result = Get(ownerId);
            return result != null;
        }
    }
}