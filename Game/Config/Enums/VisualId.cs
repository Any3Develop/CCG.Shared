using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG.Shared.Game.Config.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VisualId
    {
        None = 0
    }
}