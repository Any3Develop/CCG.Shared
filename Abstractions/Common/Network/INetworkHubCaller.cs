namespace CCG_Shared.Abstractions.Common.Network
{
    public interface INetworkHubCaller
    {
        Task InvokeAsync(object hubInstance, string target, CancellationToken token, params object[] parameters);
    }
}