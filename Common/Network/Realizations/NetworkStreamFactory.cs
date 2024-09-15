using CCG.Shared.Abstractions.Common.Network;

namespace CCG.Shared.Common.Network.Realizations
{
    public class NetworkStreamFactory : INetworkStreamFactory
    {
        public INetworkStream Create(params object[] args)
        {
            return new NetworkStream(args.OfType<Stream>().First());
        }
    }
}