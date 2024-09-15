using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Context.Providers
{
    public interface IRuntimeOrderProvider : IDisposable
    {
        IRuntimeOrderModel RuntimeModel { get; }
        void Sync(IRuntimeOrderModel runtimeModel);
        int Next();
    }
}