using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Api.Game;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface IDatabase
    {
        IConfigCollection<ObjectConfig> Objects { get; }
        IConfigCollection<EffectConfig> Effects { get; }
        IConfigCollection<PlayerConfig> Players { get; }
        IConfigCollection<StatConfig> Stats { get; }

        DatabaseModel GetModel();
    }
}