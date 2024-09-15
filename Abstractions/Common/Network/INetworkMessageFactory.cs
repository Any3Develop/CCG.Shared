using CCG.Shared.Common.Network.Data;

namespace CCG.Shared.Abstractions.Common.Network
{
    public interface INetworkMessageFactory
    {
        Message Create(object traget, params object[] args);
    }
}