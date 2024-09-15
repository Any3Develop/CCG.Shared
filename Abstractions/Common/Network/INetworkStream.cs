using CCG.Shared.Common.Network.Data;

namespace CCG.Shared.Abstractions.Common.Network
{
    public interface INetworkStream
    {
        Task<StreamResult> ReadAsync(CancellationToken token);
        Task<StreamResult> WriteAsync(byte[] buffer, CancellationToken token);
    }
}