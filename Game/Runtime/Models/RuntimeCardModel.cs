using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeCardModel : RuntimeObjectModel, IRuntimeCardModel
    {
        public int? Position { get; set; }
    }
}