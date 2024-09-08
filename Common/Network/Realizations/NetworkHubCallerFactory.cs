﻿using CCG_Shared.Abstractions.Common.Network;

namespace CCG_Shared.Common.Network.Realizations
{
    public class NetworkHubCallerFactory : INetworkHubCallerFactory
    {
        public INetworkHubCaller Create(Type hubType)
        {
            return new NetworkHubCaller(hubType);
        }
    }
}