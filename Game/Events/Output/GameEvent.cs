using CCG.Shared.Abstractions.Game.Events;
using Newtonsoft.Json;

namespace CCG.Shared.Game.Events.Output
{
    public abstract class GameEvent : IGameEvent
    {
        public int Order { get; set; }
        public string PredictionId { get; set; }
        [JsonIgnore] public bool Rollback { get; set; }
    }
}