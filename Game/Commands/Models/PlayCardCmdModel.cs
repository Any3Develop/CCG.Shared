using CCG.Shared.Game.Commands.Base;

namespace CCG.Shared.Game.Commands.Models
{
    public class PlayCardCmdModel : CommandModelBase
    {
        public int Id { get; set; }
        public int? Position { get; set; }
    }
}