using CCG_Shared.Abstractions.Game.Collections;
using CCG_Shared.Abstractions.Game.Context;
using CCG_Shared.Common.Network.Data;
using CCG_Shared.Game.Collections;
using CCG_Shared.Game.Data;

namespace CCG_Shared.Game.Context
{
    public class Database : IDatabase
    {
        public IDataCollection<ObjectData> Objects { get; } = new DataCollection<ObjectData>();
        public IDataCollection<EffectData> Effects { get; } = new DataCollection<EffectData>();
        public IDataCollection<StatData> Stats{ get; } = new DataCollection<StatData>();
        public IDataCollection<PlayerData> Players{ get; } = new DataCollection<PlayerData>();
        
        public DatabaseModel GetModel()
        {
            return new DatabaseModel
            {
                Objects = Objects.ToArray(),
                Effects = Effects.ToArray(),
                Stats = Stats.ToArray(),
                Players = Players.ToArray(),
            };
        }
    }
}