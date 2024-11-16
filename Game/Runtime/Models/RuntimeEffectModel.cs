using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeEffectModel : IRuntimeEffectModel
    {
        public int Id { get; set; }
        public string ConfigId { get; set; }
        public string OwnerId { get; set; }
        public int RuntimeOwnerId { get; set; }
        public int Value { get; set; }
        public int Lifetime { get; set; }
        
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