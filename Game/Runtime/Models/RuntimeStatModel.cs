using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeStatModel : IRuntimeStatModel
    {
        public int Id { get; set; }
        public string ConfigId { get; set; }
        public string OwnerId { get; set; }
        public StatType Type { get; set; }
        public int RuntimeOwnerId { get; set; }

        public int Max { get; set; }
        public int Value { get; set; }
    }
}