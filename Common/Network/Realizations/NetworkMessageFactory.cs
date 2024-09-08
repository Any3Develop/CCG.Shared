using CCG_Shared.Abstractions.Common.Network;
using CCG_Shared.Common.Network.Data;

namespace CCG_Shared.Common.Network.Realizations
{
    public class NetworkMessageFactory : INetworkMessageFactory
    {
        private readonly INetworkSerializer networkSerializer;
        public NetworkMessageFactory(INetworkSerializer networkSerializer)
        {
            this.networkSerializer = networkSerializer;
        }

        public Message Create(object target, params object[] args)
        {
            return new Message
            {
                Target = target?.ToString(),
                Args = args.Select(networkSerializer.Serialize).ToArray()
            };
        }
    }
}