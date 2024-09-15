using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Events.Output
{
    public class AddedObject : GameEventBase
    {
        public IRuntimeObjectModel RuntimeModel { get; set; }
    }
}