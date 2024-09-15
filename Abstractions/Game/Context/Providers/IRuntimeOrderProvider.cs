using CCG.Shared.Abstractions.Game.Runtime.Data;

namespace CCG.Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeOrderProvider : IDisposable
    {
        IRuntimeOrderData RuntimeData { get; }
        void Sync(IRuntimeOrderData runtimeData);
        int Next();
    }
}