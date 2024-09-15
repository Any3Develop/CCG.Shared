using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Common.Network.Data;
using CCG.Shared.Game.Collections;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Game.Context
{
    public class Database : IDatabase
    {
        public IConfigCollection<ObjectConfig> Objects { get; } = new ConfigCollection<ObjectConfig>();
        public IConfigCollection<EffectConfig> Effects { get; } = new ConfigCollection<EffectConfig>();
        public IConfigCollection<StatConfig> Stats{ get; } = new ConfigCollection<StatConfig>();
        public IConfigCollection<PlayerConfig> Players{ get; } = new ConfigCollection<PlayerConfig>();
        
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