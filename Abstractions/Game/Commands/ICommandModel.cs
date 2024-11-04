namespace CCG.Shared.Abstractions.Game.Commands
{
    public interface ICommandModel
    {
        string Name { get; }
        string CommandId { get; }
        string PredictionId { get; }
        string ExecutorId { get; }
        bool IsNested { get; }
    }
}