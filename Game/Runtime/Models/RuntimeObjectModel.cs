using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime.Models
{
    public abstract class RuntimeObjectModel : IRuntimeObjectModel
    {
        public int Id { get; set; }
        public string ConfigId { get; set; }
        public string OwnerId { get; set; }
        public List<string> EffectIds { get; set; } = new();
        public List<IRuntimeEffectModel> Applied { get; set; } = new();
        public List<IRuntimeStatModel> Stats { get; set; } = new();
        public ObjectState State { get; set; }
        public ObjectState PreviousState { get; set; }

        public override bool Equals(object obj)
        {
            return obj is IRuntimeModelBase other && other.Id == Id;
        }
        
        public override int GetHashCode()
        {
            return Id;
        }
    }
}