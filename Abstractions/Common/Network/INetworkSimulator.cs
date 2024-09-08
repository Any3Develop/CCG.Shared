namespace CCG_Shared.Abstractions.Common.Network
{
    public interface INetworkSimulator
    {
        Task TickOptimizerAsync(CancellationToken token);
        Task WaitRandomAsync(CancellationToken token);
    }
}