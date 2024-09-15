using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeIdProvider : IDisposable
    {
        IRuntimeIdModel RuntimeModel { get; }
        void Sync(IRuntimeIdModel runtimeModel);
        int Next();
    }
}