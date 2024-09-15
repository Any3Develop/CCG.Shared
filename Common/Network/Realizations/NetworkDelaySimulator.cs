using CCG.Shared.Abstractions.Common.Network;

namespace CCG.Shared.Common.Network.Realizations
{
    public class NetworkDelaySimulator : INetworkSimulator
    {
        private const int TickDelayMs = 500;
        private readonly Random delayGenerator = new();

        public Task TickOptimizerAsync(CancellationToken token)
        {
            return Task.Delay(TickDelayMs, token);
        }
        
        public Task WaitRandomAsync(CancellationToken token)
        {
            return Task.Delay(delayGenerator.Next(1000), token);
        }
    }
}