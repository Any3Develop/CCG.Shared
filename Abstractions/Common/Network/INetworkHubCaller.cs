namespace CCG.Shared.Abstractions.Common.Network
{
    public interface INetworkHubCaller
    {
        Task InvokeAsync(object hubInstance, string target, CancellationToken token, params object[] parameters);
    }
}