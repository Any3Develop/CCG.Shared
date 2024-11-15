using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Abstractions.Game.Collections
{
    public interface IObjectsCollection : IRuntimeCollection<IRuntimeObject>
    {
        IEnumerable<TRuntime> GetAll<TRuntime>(
            ObjectState? state = null, 
            string ownerId = null, 
            bool asQuery = false, 
            params int[] runtimeIds) 
            where TRuntime : IRuntimeObject;
        
        int GetOccupiedTableSpace(string ownerId); // TODO: move to conditions
    }
}