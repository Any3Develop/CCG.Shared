using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeIdModel : IRuntimeIdModel
    {
        public int NextId { get; set; }
    }
}