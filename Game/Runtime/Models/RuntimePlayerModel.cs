using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimePlayerModel : IRuntimePlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public string ConfigId { get; set; }
        public bool Ready { get; set; }
        public bool IsFirst { get; set; }
        public List<IRuntimeStatModel> Stats { get; set; } = new();
        
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