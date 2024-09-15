using CCG.Shared.Abstractions.Game.Commands;

namespace CCG.Shared.Game.Commands.Base
{
    public class CommandModelBase : ICommandModel
    {
        public string TypeName { get; set; }
        public string CommandId { get; set; }
        public string PredictionId { get; set; }
        public bool IsNested { get; set; }
    }
}