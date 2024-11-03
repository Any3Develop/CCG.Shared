using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IRuntimeTimerFactory : IRuntimeFactory<IRuntimeTimer, IRuntimeTimerModel>
    {
        IRuntimeTimerModel CreateModel(bool notify = false);
    }
}