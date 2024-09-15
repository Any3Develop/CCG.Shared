using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Context;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface IDatabase
    {
        IConfigCollection<ObjectConfig> Objects { get; }
        IConfigCollection<EffectConfig> Effects { get; }
        IConfigCollection<StatConfig> Stats { get; }
        IConfigCollection<PlayerConfig> Players { get; }

        DatabaseModel GetModel();
    }
}