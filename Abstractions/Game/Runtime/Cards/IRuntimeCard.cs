using CCG_Shared.Abstractions.Game.Runtime.Data;
using CCG_Shared.Abstractions.Game.Runtime.Objects;
using CCG_Shared.Game.Data;

namespace CCG_Shared.Abstractions.Game.Runtime.Cards
{
    public interface IRuntimeCard : IRuntimeObject
    {
        new CardData Data { get;}
        new IRuntimeCardData RuntimeData { get; }

        void SetPosition(int? value, bool notify = true);
    }
}