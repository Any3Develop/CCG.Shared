using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG.Shared.Game.Enums
{
    [JsonConverter(typeof(StringEnumConverter)), Flags]
    public enum ObjectType
    {
        None = 0,
        Creature = 2,
        Spell = 4,
        Hero = 8,
        
        All = -1,
        Cards = Creature | Spell
    }
}