using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CCG.Shared.Game.Enums
{
    [JsonConverter(typeof(StringEnumConverter)), Flags]
    public enum TimerState
    {
        NotStarted = 2,
        Mulligan = 4,
        GamePlay = 8,
        Ending = 16,
        Paused = 32,
        Ended = 64,
    }
}