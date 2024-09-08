namespace CCG_Shared.Abstractions.Common.Network
{
    public interface INetworkHubCallerFactory
    {
        INetworkHubCaller Create(Type hubType);
    }
}