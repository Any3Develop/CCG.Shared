﻿using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Runtime.Players;

namespace CCG.Shared.Game.Collections
{
    public class PlayersCollection : RuntimeCollectionBase<IRuntimePlayer>, IPlayersCollection
    {
        protected override int GetId(IRuntimePlayer value) =>
            value?.RuntimeData?.Id ?? int.MinValue;
        
        public IRuntimePlayer Get(string ownerId)
        {
            return GetFirst(x => x.RuntimeData.OwnerId == ownerId);
        }

        public IRuntimePlayer GetOpposite(string ownerId)
        {
            return GetFirst(x => x.RuntimeData.OwnerId != ownerId);
        }

        public IRuntimePlayer GetOpposite(IRuntimePlayer runtimePlayer)
        {
            return runtimePlayer?.RuntimeData != null 
                ? GetFirst(x => x.RuntimeData.OwnerId != runtimePlayer.RuntimeData.OwnerId) 
                : default;
        }

        public bool TryGet(string ownerId, out IRuntimePlayer result)
        {
            result = Get(ownerId);
            return result != null;
        }

        public bool Contains(string ownerId)
        {
            return Get(ownerId) != null;
        }
    }
}