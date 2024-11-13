using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventProcessors;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Context
{
    public interface IContext
    {
        #region Static Context
        ISystemTimers SystemTimers { get; }
        ISharedTime SharedTime { get; }
        ISharedConfig Config { get; }
        IDatabase Database { get; }
        #endregion

        #region Runtime Context
        IRuntimeContextModel RuntimeData { get; }
        IRuntimeTimer RuntimeTimer { get; set; }
        IObjectsCollection ObjectsCollection { get; }
        IPlayersCollection PlayersCollection { get; }
        IRuntimeRandomProvider RuntimeRandomProvider { get; }
        IRuntimeOrderProvider RuntimeOrderProvider { get; }
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
        IRuntimeObjectFactory ObjectFactory { get; }
        IRuntimePlayerFactory PlayerFactory { get; }
        IRuntimeEffectFactory EffectFactory { get; }
        IRuntimeStatFactory StatFactory { get; }
        IRuntimeTimerFactory TimerFactory { get; }
        IContextFactory ContextFactory { get; }
        #endregion

        IContext Sync(IRuntimeContextModel value);
    }
}