using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeIdProvider : IDisposable
    {
        IRuntimeIdData RuntimeData { get; }
        void Sync(IRuntimeIdData runtimeData);
        int Next();
    }
}