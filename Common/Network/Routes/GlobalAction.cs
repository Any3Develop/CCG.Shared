using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG_Shared.Common.Network.Routes
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GlobalAction
    {
        CloseConnection,
        Error,
        Ping,
    }
}