using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG.Shared.Game.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TimerState
    {
        NotStarted = 0,
        Mulligan,
        Game,
        GameTurnEnding,
        End,
    }
}