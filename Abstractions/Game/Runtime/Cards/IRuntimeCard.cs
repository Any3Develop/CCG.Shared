using CCG.Shared.Abstractions.Game.Runtime.Data;
using CCG.Shared.Abstractions.Game.Runtime.Objects;
using CCG.Shared.Game.Data;

namespace CCG.Shared.Abstractions.Game.Runtime.Cards
{
    public interface IRuntimeCard : IRuntimeObject
    {
        new CardData Data { get;}
        new IRuntimeCardData RuntimeData { get; }

        void SetPosition(int? value, bool notify = true);
    }
}