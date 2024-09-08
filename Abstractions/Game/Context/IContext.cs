using CCG_Shared.Abstractions.Game.Collections;
using CCG_Shared.Abstractions.Game.Context.EventSource;
using CCG_Shared.Abstractions.Game.Context.Providers;
using CCG_Shared.Abstractions.Game.Factories;

namespace CCG_Shared.Abstractions.Game.Context
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