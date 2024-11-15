using CCG.Shared.Abstractions.Game.Runtime;

namespace CCG.Shared.Abstractions.Game.Context.Processors
{
    public interface ICroupierProcessor
    {
        IEnumerable<IRuntimeCard> Start(string ownerId);
        IEnumerable<IRuntimeCard> GiveCards(string ownerId, int count);
        IEnumerable<IRuntimeCard> GiveCards(string ownerId, params int[] runtimeIds);
        void Shuffle(string ownerId);
    }
}