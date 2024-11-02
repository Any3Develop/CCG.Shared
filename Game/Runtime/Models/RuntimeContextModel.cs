using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Context.Session;

namespace CCG.Shared.Game.Runtime.Models
{
    public class RuntimeContextModel : IRuntimeContextModel
    {
        public string Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? ReadyTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<SessionPlayer> Players { get; set; }
    }
}