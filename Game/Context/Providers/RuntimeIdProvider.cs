using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Context.Providers
{
    public class RuntimeIdProvider : IRuntimeIdProvider
    {
        public IRuntimeIdModel RuntimeModel { get; private set; }
        private object orderLock;
        
        public void Sync(IRuntimeIdModel runtimeModel)
        {
            orderLock ??= new object();
            lock (orderLock)
            {
                RuntimeModel = runtimeModel ?? throw new Exception($"{GetType().Name} not initialized.");
            }
        }

        public int Next()
        {
            if (orderLock == null)
                throw new Exception($"{GetType().Name} not initialized.");

            lock (orderLock)
            {
                return RuntimeModel.NextId ++;
            }
        }

        public void Dispose()
        {
            orderLock = null;
            RuntimeModel = null;
        }
    }
}