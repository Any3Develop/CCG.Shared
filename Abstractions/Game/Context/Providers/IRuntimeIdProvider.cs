using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeIdProvider : IDisposable
    {
        IRuntimeIdData RuntimeData { get; }
        void Sync(IRuntimeIdData runtimeData);
        int Next();
    }
}