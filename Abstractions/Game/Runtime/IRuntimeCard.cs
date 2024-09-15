using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;

namespace CCG.Shared.Abstractions.Game.Runtime
{
    public interface IRuntimeCard : IRuntimeObject
    {
        new CardConfig Config { get;}
        new IRuntimeCardModel RuntimeModel { get; }

        void SetPosition(int? value, bool notify = true);
    }
}