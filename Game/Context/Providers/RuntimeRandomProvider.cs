using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Context.Providers
{
    public class RuntimeRandomProvider : IRuntimeRandomProvider
    {
        public IRuntimeRandomModel RuntimeModel { get; private set; }
        private object randomLock;
        private Random random;

        public void Sync(IRuntimeRandomModel runtimeModel)
        {
            randomLock ??= new object();
            lock (randomLock)
            {
                RuntimeModel = runtimeModel ?? throw new Exception($"{GetType().Name} not initialized.");
                random = new Random(runtimeModel.Seed);
            }
        }

        public int Next()
        {
            if (randomLock == null)
                throw new Exception($"{GetType().Name} not initialized.");

            lock (randomLock)
            {
                RuntimeModel.Current = random.Next();
                return RuntimeModel.Current;
            }
        }

        public void Dispose()
        {
            randomLock = null;
            RuntimeModel = null;
            random = null;
        }
    }
}