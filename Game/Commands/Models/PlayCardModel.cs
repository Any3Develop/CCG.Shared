using CCG_Shared.Game.Commands.Base;

namespace CCG_Shared.Game.Commands.Models
{
    public class PlayCardModel : CommandModelBase
    {
        public int Id { get; set; }
        public int? Position { get; set; }
    }
}