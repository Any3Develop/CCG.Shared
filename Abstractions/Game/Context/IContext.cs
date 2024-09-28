using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventProcessors;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface IContext
    {
        ISharedConfig Config { get; }
        IDatabase Database { get; }
        IObjectsCollection ObjectsCollection { get; }
        IPlayersCollection PlayersCollection { get; }
        IRuntimeOrderProvider RuntimeOrderProvider { get; }
        IRuntimeRandomProvider RuntimeRandomProvider { get; }
        IRuntimeIdProvider RuntimeIdProvider { get; }
        ICommandProcessor CommandProcessor { get; }
        IGameQueueCollector GameQueueCollector { get; }
        IEventsSource EventSource { get; }
        IEventPublisher EventPublisher { get; }

        #region Factories

        IRuntimeObjectFactory ObjectFactory { get; }
        IRuntimeEffectFactory EffectFactory { get; }
        IRuntimeStatFactory StatFactory { get; }

        #endregion
    }
}