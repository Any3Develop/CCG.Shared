using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class Inititalize : GameEventBase
    {
        public List<IRuntimeBaseModel> Runtimes { get; set; }
    }
}