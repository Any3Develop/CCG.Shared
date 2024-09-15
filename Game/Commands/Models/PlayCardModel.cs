using CCG.Shared.Game.Commands.Base;

namespace CCG.Shared.Game.Commands.Models
{
    public class PlayCardModel : CommandModelBase
    {
        public int Id { get; set; }
        public int? Position { get; set; }
    }
}