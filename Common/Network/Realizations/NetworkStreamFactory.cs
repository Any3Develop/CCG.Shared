using CCG_Shared.Abstractions.Common.Network;

namespace CCG_Shared.Common.Network.Realizations
{
    public class NetworkStreamFactory : INetworkStreamFactory
    {
        public INetworkStream Create(params object[] args)
        {
            return new NetworkStream(args.OfType<Stream>().First());
        }
    }
}