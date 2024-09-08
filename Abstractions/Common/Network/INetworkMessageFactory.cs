using CCG_Shared.Common.Network.Data;

namespace CCG_Shared.Abstractions.Common.Network
{
    public interface INetworkMessageFactory
    {
        Message Create(object traget, params object[] args);
    }
}