using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG.Shared.Common.Network.Routes
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GlobalAction
    {
        CloseConnection,
        Error,
        Ping,
    }
}