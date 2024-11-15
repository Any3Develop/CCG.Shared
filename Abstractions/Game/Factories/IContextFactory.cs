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
        IObjectsCollection CreateObjectsCollection(params object[] args);
        IEffectsCollection CreateEffectsCollection(params object[] args);
        IStatsCollection CreateStatsCollection(params object[] args);
        IPlayersCollection CreatePlayersCollection(params object[] args);
        #endregion

        #region Logic
        IEventsSource CreateEventsSource(params object[] args);
        IEventPublisher CreateEventPublisher(params object[] args);
        IRuntimeIdProvider CreateRuntimeIdProvider(params object[] args);
        IRuntimeOrderProvider CreateRuntimeOrderProvider(params object[] args);
        IRuntimeRandomProvider CreateRuntimeRandomProvider(params object[] args);
        ICommandProcessor CreateCommandProcessor(params object[] args);
        IGameQueueCollector CreateGameQueueCollector(params object[] args);
        IObjectEventProcessor CreateObjectEventProcessor(params object[] args);
        IContextEventProcessor CreateContextEventProcessor(params object[] args);
        IGameEventProcessor CreateGameEventProcessor(params object[] args);
        ICroupierProcessor CreateCroupierProcessor(params object[] args);
        #endregion

        #region Factories
        ICommandFactory CreateCommandFactory(params object[] args);
        IRuntimeStatFactory CreateStatFactory(params object[] args);
        IRuntimeObjectFactory CreateObjectFactory(params object[] args);
        IRuntimePlayerFactory CreatePlayerFactory(params object[] args);
        IRuntimeEffectFactory CreateEffectFactory(params object[] args);
        IRuntimeTimerFactory CreateTimerFactory(params object[] args);
        #endregion

        #region Context

        IContext CreateContext(params object[] args);

        #endregion
    }
}