using CCG_Shared.Abstractions.Game.Runtime.Data;

namespace CCG_Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeRandomProvider : IDisposable
    {
        IRuntimeRandomData RuntimeData { get; }
        void Sync(IRuntimeRandomData runtimeData);
        int Next();
    }
}