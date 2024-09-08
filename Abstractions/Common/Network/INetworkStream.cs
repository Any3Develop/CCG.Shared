using CCG_Shared.Common.Network.Data;

namespace CCG_Shared.Abstractions.Common.Network
{
    public interface INetworkStream
    {
        Task<StreamResult> ReadAsync(CancellationToken token);
        Task<StreamResult> WriteAsync(byte[] buffer, CancellationToken token);
    }
}