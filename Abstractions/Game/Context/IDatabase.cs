using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Common.Network.Data;
using CCG.Shared.Game.Data;

namespace CCG.Shared.Abstractions.Game.Context
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