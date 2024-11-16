using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeTimerFactory
    {
        IRuntimeTimerModel CreateModel();

        IRuntimeTimer Create(IRuntimeTimerModel runtimeModel);
    }
}