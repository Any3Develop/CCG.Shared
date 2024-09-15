using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Abstractions.Game.Collections
{
    public interface IObjectsCollection : IRuntimeCollection<IRuntimeObject>
    {
        int GetOccupiedTableSpace(string ownerId); // TODO: move to conditions
    }
}