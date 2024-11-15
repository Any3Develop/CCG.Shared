using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Context.Processors
{
    public class CroupierProcessor : ICroupierProcessor
    {
        private readonly IContext context;
        public CroupierProcessor(IContext context)
        {
            this.context = context;
        }

        public IEnumerable<IRuntimeCard> Start(string ownerId)
        {
            return Enumerable.Empty<IRuntimeCard>(); // TODO
        }

        public IEnumerable<IRuntimeCard> GiveCards(string ownerId, int count)
        {
            var cards = context.ObjectsCollection
                .GetAll<IRuntimeCard>(ObjectState.InDeck, ownerId, asQuery: true)
                .Take(count)
                .ToArray();

            MoveToHand(cards);
            return cards;
        }

        public IEnumerable<IRuntimeCard> GiveCards(string ownerId, params int[] runtimeIds)
        {
            var cards = context.ObjectsCollection
                .GetAll<IRuntimeCard>(ObjectState.InDeck, ownerId, asQuery: true, runtimeIds)
                .ToArray();

            MoveToHand(cards);
            return cards;
        }

        public void Shuffle(string ownerId)
        {
            // TODO shuffle
        }

        private static void MoveToHand(IEnumerable<IRuntimeCard> cards, bool notify = true)
        {
            foreach (var runtimeCard in cards)
                runtimeCard.SetState(ObjectState.InHand, notify: notify);
        }
    }
}