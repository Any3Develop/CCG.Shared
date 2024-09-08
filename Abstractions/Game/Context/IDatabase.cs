using CCG_Shared.Abstractions.Game.Collections;
using CCG_Shared.Common.Network.Data;
using CCG_Shared.Game.Data;

namespace CCG_Shared.Abstractions.Game.Context
{
    public interface IDatabase
    {
        IDataCollection<ObjectData> Objects { get; }
        IDataCollection<EffectData> Effects { get; }
        IDataCollection<StatData> Stats { get; }
        IDataCollection<PlayerData> Players { get; }

        DatabaseModel GetModel();
    }
}