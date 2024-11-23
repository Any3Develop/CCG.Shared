using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG.Shared.Game.Enums
{
    [JsonConverter(typeof(StringEnumConverter)), Flags]
    public enum DamageType
    {
        None = 0,
        Direct = 2,
        Spell = 4,
        CounterAttack = 8,
    }
}