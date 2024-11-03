using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Api.Game;
using CCG.Shared.Game.Collections;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Game.Context
{
    public class Database : IDatabase
    {
        public IConfigCollection<ObjectConfig> Objects { get; } = new ConfigCollection<ObjectConfig>();
        public IConfigCollection<EffectConfig> Effects { get; } = new ConfigCollection<EffectConfig>();
        public IConfigCollection<StatConfig> Stats { get; } = new ConfigCollection<StatConfig>();

        public DatabaseModel GetModel()
        {
            return new DatabaseModel
            {
                Objects = Objects.ToArray(),
                Effects = Effects.ToArray(),
                Stats = Stats.ToArray(),
            };
        }
    }
}