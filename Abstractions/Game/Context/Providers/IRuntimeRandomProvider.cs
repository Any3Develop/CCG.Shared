using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeRandomProvider : IDisposable
    {
        IRuntimeRandomModel RuntimeModel { get; }
        void Sync(IRuntimeRandomModel runtimeModel);
        int Next();
    }
}