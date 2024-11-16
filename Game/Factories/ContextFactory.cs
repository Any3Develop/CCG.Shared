using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Game.Collections;
using CCG.Shared.Game.Commands.Base;
using CCG.Shared.Game.Context;
using CCG.Shared.Game.Context.EventProcessors;
using CCG.Shared.Game.Context.Processors;
using CCG.Shared.Game.Context.Providers;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime.Effects;

namespace CCG.Shared.Game.Factories
{
    public class ContextFactory : IContextFactory
    {
        private readonly IDatabase database;
        private readonly ISharedConfig config;
        private readonly ISharedTime sharedTime;
        private readonly ISystemTimers systemTimers;
        private readonly IEventsSourceFactory eventsSourceFactory;
        private readonly ITypeCollection<LogicId, RuntimeEffectBase> logicTypeCollection;
        private readonly ITypeCollection<string, Command> commandTypeCollection;

        public ContextFactory(
            IDatabase database,
            ISharedConfig config,
            ISharedTime sharedTime,
            ISystemTimers systemTimers,
            IEventsSourceFactory eventsSourceFactory,
            ITypeCollection<LogicId, RuntimeEffectBase> logicTypeCollection, 
            ITypeCollection<string, Command> commandTypeCollection)
        {
            this.database = database;
            this.config = config;
            this.sharedTime = sharedTime;
            this.systemTimers = systemTimers;
            this.eventsSourceFactory = eventsSourceFactory;
            this.logicTypeCollection = logicTypeCollection;
            this.commandTypeCollection = commandTypeCollection;
        }

        #region Collections
        public IObjectsCollection CreateObjectsCollection(params object[] args)
        {
            return new ObjectsCollection(GetRequiredArgument<IEventPublisher>(args));
        }

        public IEffectsCollection CreateEffectsCollection(params object[] args)
        {
            return new EffectsCollection(GetRequiredArgument<IEventPublisher>(args));
        }

        public IStatsCollection CreateStatsCollection(params object[] args)
        {
            return new StatsCollection(GetRequiredArgument<IEventPublisher>(args));
        }

        public IPlayersCollection CreatePlayersCollection(params object[] args)
        {
            return new PlayersCollection();
        }
        #endregion

        #region Logic
        public IEventsSource CreateEventsSource(params object[] args)
        {
            return eventsSourceFactory.CreateSource(args);
        }

        public IEventPublisher CreateEventPublisher(params object[] args)
        {
            return eventsSourceFactory.CreatePublisher(args);
        }

        public IRuntimeIdProvider CreateRuntimeIdProvider(params object[] args)
        {
            return new RuntimeIdProvider();
        }

        public IRuntimeOrderProvider CreateRuntimeOrderProvider(params object[] args)
        {
            return new RuntimeOrderProvider();
        }
        
        public IRuntimeRandomProvider CreateRuntimeRandomProvider(params object[] args)
        {
            return new RuntimeRandomProvider();
        }

        public ICommandProcessor CreateCommandProcessor(params object[] args)
        {
            var context = GetRequiredArgument<IContext>(args);
            return new CommandProcessor(
                context,
                CreateCommandFactory(context));
        }

        public IGameQueueCollector CreateGameQueueCollector(params object[] args)
        {
            return new GameQueueCollector(
                GetRequiredArgument<IEventsSource>(args),
                GetRequiredArgument<IEventPublisher>(args),
                GetRequiredArgument<IRuntimeOrderProvider>(args));
        }

        public IObjectEventProcessor CreateObjectEventProcessor(params object[] args)
        {
            return new ObjectEventProcessor(GetRequiredArgument<IGameQueueCollector>(args));
        }

        public IContextEventProcessor CreateContextEventProcessor(params object[] args)
        {
            return new ContextEventProcessor(
                GetRequiredArgument<IEventsSource>(args),
                GetRequiredArgument<IRuntimeIdProvider>(args),
                GetRequiredArgument<IRuntimeOrderProvider>(args),
                GetRequiredArgument<IRuntimeRandomProvider>(args));
        }

        public IGameEventProcessor CreateGameEventProcessor(params object[] args)
        {
            return new GameEventProcessor(GetRequiredArgument<IContext>(args));
        }
        
        public ICroupierProcessor CreateCroupierProcessor(params object[] args)
        {
            return new CroupierProcessor(GetRequiredArgument<IContext>(args));
        }
        
        public ITurnProcessor CreateTurnProcessor(params object[] args)
        {
            return new TurnProcessor(GetRequiredArgument<IContext>(args));
        }

        #endregion

        #region Factories
        public ICommandFactory CreateCommandFactory(params object[] args)
        {
            return new CommandFactory(
                GetRequiredArgument<IContext>(),
                commandTypeCollection);
        }
        
        public IRuntimeStatFactory CreateStatFactory(params object[] args)
        {
            return new RuntimeStatFactory(
                database, 
                GetRequiredArgument<IObjectsCollection>(), 
                GetRequiredArgument<IPlayersCollection>(), 
                GetRequiredArgument<IRuntimeIdProvider>());
        }

        public IRuntimeObjectFactory CreateObjectFactory(params object[] args)
        {
            return new RuntimeObjectFactory(
                database, 
                GetRequiredArgument<IObjectsCollection>(), 
                GetRequiredArgument<IRuntimeIdProvider>(),
                GetRequiredArgument<IRuntimeStatFactory>(),
                GetRequiredArgument<IRuntimeEffectFactory>(),
                this);
        }

        public IRuntimePlayerFactory CreatePlayerFactory(params object[] args)
        {
            return new RuntimePlayerFactory(
                config,
                GetRequiredArgument<IPlayersCollection>(),
                GetRequiredArgument<IRuntimeIdProvider>(),
                GetRequiredArgument<IRuntimeStatFactory>(),
                this);
        }

        public IRuntimeEffectFactory CreateEffectFactory(params object[] args)
        {
            return new RuntimeEffectFactory(
                database, 
                GetRequiredArgument<IObjectsCollection>(), 
                GetRequiredArgument<IRuntimeIdProvider>(),
                logicTypeCollection);
        }

        public IRuntimeTimerFactory CreateTimerFactory(params object[] args)
        {
            return new RuntimeTimerFactory(
                config, 
                GetRequiredArgument<IContext>());
        }

        #endregion

        #region Context
        
        public virtual IContext CreateContext(params object[] args)
        {
            var eventsSource = CreateEventsSource();
            var eventPublisher = CreateEventPublisher(eventsSource);
            var objectsCollection = CreateObjectsCollection(eventPublisher);
            var playersCollection = CreatePlayersCollection();

            var randomProvider = CreateRuntimeRandomProvider();
            var orderProvider = CreateRuntimeOrderProvider();
            var idProvider = CreateRuntimeIdProvider();

            var statFactory = CreateStatFactory(objectsCollection, idProvider, playersCollection);
            var effectFactory = CreateEffectFactory(objectsCollection, idProvider);
            var gameQueueCollector = CreateGameQueueCollector(eventsSource, eventPublisher, orderProvider);

            var context = new SharedContext
            {
                SystemTimers = systemTimers,
                SharedTime = sharedTime,
                Config = config,
                Database = database,
                ObjectsCollection = objectsCollection,
                PlayersCollection = playersCollection,
                RuntimeRandomProvider = randomProvider,
                RuntimeOrderProvider = orderProvider,
                RuntimeIdProvider = idProvider,

                ObjectEventProcessor = CreateObjectEventProcessor(gameQueueCollector),
                ContextEventProcessor = CreateContextEventProcessor(eventsSource, idProvider, orderProvider, randomProvider),
                GameQueueCollector = gameQueueCollector,
                EventPublisher = eventPublisher,
                EventSource = eventsSource,

                ObjectFactory = CreateObjectFactory(objectsCollection, idProvider, statFactory, effectFactory),
                PlayerFactory = CreatePlayerFactory(playersCollection, idProvider, statFactory),
                EffectFactory = effectFactory,
                StatFactory = statFactory,
                ContextFactory = this,
            };

            context.TurnProcessor = CreateTurnProcessor(context);
            context.CroupierProcessor = CreateCroupierProcessor(context);
            context.GameEventProcessor = CreateGameEventProcessor(context);
            context.CommandProcessor = CreateCommandProcessor(context);
            context.TimerFactory = CreateTimerFactory(context);
            return context;
        }

        private static T GetRequiredArgument<T>(params object[] args)
        {
            return (T) args.FirstOrDefault(x => x is T);
        }
        #endregion
    }
}