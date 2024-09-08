using CCG_Shared.Abstractions.Game.Runtime.Objects;

namespace CCG_Shared.Abstractions.Game.Collections
{
    public interface IObjectsCollection : IRuntimeCollection<IRuntimeObject>
    {
        int GetOccupiedTableSpace(string ownerId);
    }
}