using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeOrderProvider : IDisposable
    {
        IRuntimeOrderData RuntimeData { get; }
        void Sync(IRuntimeOrderData runtimeData);
        int Next();
    }
}