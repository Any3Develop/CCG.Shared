using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeRandomModel : IRuntimeRandomModel
    {
        public int Seed { get; set; }
        public int Current { get; set; }
    }
}