using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config.Enums;

namespace CCG.Shared.Game.Runtime.Models
{
    public abstract class RuntimeObjectModel : IRuntimeObjectModel
    {
        public int Id { get; set; } = -1;
        public string ConfigId { get; set; }
        public string OwnerId { get; set; }
        public List<string> EffectIds { get; set; } = new();
        public List<IRuntimeEffectModel> Applied { get; set; } = new();
        public List<IRuntimeStatModel> Stats { get; set; } = new();
        public ObjectState State { get; set; } = 0;
        public ObjectState PreviousState { get; set; } = 0;
    }
}