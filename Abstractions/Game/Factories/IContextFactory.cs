using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Context.Providers;

namespace CCG.Shared.Abstractions.Game.Factories
{
    public interface IContextFactory
    {
        #region Collections
        IObjectsCollection CreateObjectsCollection(IEventPublisher eventPublisher);
        IEffectsCollection CreateEffectsCollection(IEventPublisher eventPublisher);
        IStatsCollection CreateStatsCollection(IEventPublisher eventPublisher);
        IPlayersCollection CreatePlayersCollection();
        #endregion

        #region Logic
        IEventsSource CreateEventsSource(params object[] args);
        IEventPublisher CreateEventPublisher(params object[] args);
        IRuntimeIdProvider CreateRuntimeIdProvider();
        IRuntimeOrderProvider CreateRuntimeOrderProvider();
        IRuntimeRandomProvider CreateRuntimeRandomProvider();
        ICommandProcessor CreateCommandProcessor(IContext context);
        IGameQueueCollector CreateGameQueueCollector(IContext context);
        IObjectEventProcessor CreateObjectEventProcessor(IContext context);
        IContextEventProcessor CreateContextEventProcessor(IContext context);
        IGameEventProcessor CreateGameEventProcessor(IContext context);
        ICroupierProcessor CreateCroupierProcessor(IContext context);
        ITurnProcessor CreateTurnProcessor(IContext context);
        IWinConditionProcessor CreateWinConditionProcessor(IContext context);
        #endregion

        #region Factories
        ICommandFactory CreateCommandFactory(IContext context);
        IRuntimeStatFactory CreateStatFactory(IContext context);
        IRuntimeObjectFactory CreateObjectFactory(IContext context);
        IRuntimePlayerFactory CreatePlayerFactory(IContext context);
        IRuntimeEffectFactory CreateEffectFactory(IContext context);
        IRuntimeTimerFactory CreateTimerFactory(IContext context);
        #endregion

        #region Context
        IContext CreateContext();
        #endregion
    }
}