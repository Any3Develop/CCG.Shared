using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeOrderModel : IRuntimeOrderModel
    {
        public int NextOrder { get; set; }
    }
}