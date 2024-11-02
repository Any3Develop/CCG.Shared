using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventProcessors;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface IContext
    {
        #region Static Context
        ISharedTime SharedTime { get; }
        ISharedConfig Config { get; }
        IDatabase Database { get; }
        #endregion

        #region Runtime Context
        IRuntimeContextModel RuntimeData { get; }
        IObjectsCollection ObjectsCollection { get; }
        IPlayersCollection PlayersCollection { get; }
        IRuntimeOrderProvider RuntimeOrderProvider { get; }
        IRuntimeRandomProvider RuntimeRandomProvider { get; }
        IRuntimeIdProvider RuntimeIdProvider { get; }
        #endregion

        #region Logic Context
        IObjectEventProcessor ObjectEventProcessor { get; }
        IContextEventProcessor ContextEventProcessor { get; }
        IGameEventProcessor GameEventProcessor { get; }
        ICommandProcessor CommandProcessor { get; }
        IGameQueueCollector GameQueueCollector { get; }
        IEventPublisher EventPublisher { get; }
        IEventsSource EventSource { get; }
        #endregion
        
        #region Factories
        IRuntimeObjectFactory ObjectFactory { get; }
        IRuntimeEffectFactory EffectFactory { get; }
        IRuntimeStatFactory StatFactory { get; }
        #endregion

        IContext Sync(IRuntimeContextModel value, bool notify = false);
    }
}