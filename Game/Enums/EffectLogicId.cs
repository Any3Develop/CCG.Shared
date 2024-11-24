using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG.Shared.Game.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EffectLogicId
    {
        None = 0,
        DirectAttack,
        CounterAttack,
    }
}