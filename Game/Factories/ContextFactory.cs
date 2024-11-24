using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Game.Collections;
using CCG.Shared.Game.Commands.Base;
using CCG.Shared.Game.Context;
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
        private readonly ITypeCollection<EffectLogicId, RuntimeEffectBase> logicTypeCollection;
        private readonly ITypeCollection<string, Command> commandTypeCollection;

        public ContextFactory(
            IDatabase database,
            ISharedConfig config,
            ISharedTime sharedTime,
            ISystemTimers systemTimers,
            IEventsSourceFactory eventsSourceFactory,
            ITypeCollection<EffectLogicId, RuntimeEffectBase> logicTypeCollection, 
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
        public IObjectsCollection CreateObjectsCollection(IEventPublisher eventPublisher)
        {
            return new ObjectsCollection(eventPublisher);
        }

        public IEffectsCollection CreateEffectsCollection(IEventPublisher eventPublisher)
        {
            return new EffectsCollection(eventPublisher);
        }

        public IStatsCollection CreateStatsCollection(IEventPublisher eventPublisher)
        {
            return new StatsCollection(eventPublisher);
        }

        public IPlayersCollection CreatePlayersCollection()
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

        public IRuntimeIdProvider CreateRuntimeIdProvider()
        {
            return new RuntimeIdProvider();
        }

        public IRuntimeOrderProvider CreateRuntimeOrderProvider()
        {
            return new RuntimeOrderProvider();
        }
        
        public IRuntimeRandomProvider CreateRuntimeRandomProvider()
        {
            return new RuntimeRandomProvider();
        }

        public ICommandProcessor CreateCommandProcessor(IContext context)
        {
            return new CommandProcessor(context);
        }

        public IGameQueueCollector CreateGameQueueCollector(IContext context)
        {
            return new GameQueueCollector(context);
        }

        public IObjectEventProcessor CreateObjectEventProcessor(IContext context)
        {
            return new ObjectEventProcessor(context);
        }

        public IContextEventProcessor CreateContextEventProcessor(IContext context)
        {
            return new ContextEventProcessor(context);
        }

        public IGameEventProcessor CreateGameEventProcessor(IContext context)
        {
            return new GameEventProcessor(context);
        }
        
        public ICroupierProcessor CreateCroupierProcessor(IContext context)
        {
            return new CroupierProcessor(context);
        }
        
        public ITurnProcessor CreateTurnProcessor(IContext context)
        {
            return new TurnProcessor(context);
        }
        
        public IWinConditionProcessor CreateWinConditionProcessor(IContext context)
        {
            return new WinConditionProcessor(context);
        }

        #endregion

        #region Factories
        public ICommandFactory CreateCommandFactory(IContext context)
        {
            return new CommandFactory(context, commandTypeCollection);
        }
        
        public IRuntimeStatFactory CreateStatFactory(IContext context)
        {
            return new RuntimeStatFactory(context);
        }

        public IRuntimeObjectFactory CreateObjectFactory(IContext context)
        {
            return new RuntimeObjectFactory(context);
        }

        public IRuntimePlayerFactory CreatePlayerFactory(IContext context)
        {
            return new RuntimePlayerFactory(context);
        }

        public IRuntimeEffectFactory CreateEffectFactory(IContext context)
        {
            return new RuntimeEffectFactory(context, logicTypeCollection);
        }

        public IRuntimeTimerFactory CreateTimerFactory(IContext context)
        {
            return new RuntimeTimerFactory(context);
        }

        #endregion

        #region Context
        
        public virtual IContext CreateContext()
        {
            var eventsSource = CreateEventsSource();
            var eventPublisher = CreateEventPublisher(eventsSource);
            
            var context = new SharedContext
            {
                SystemTimers = systemTimers,
                SharedTime = sharedTime,
                Config = config,
                Database = database,
                ObjectsCollection = CreateObjectsCollection(eventPublisher),
                PlayersCollection = CreatePlayersCollection(),
                RuntimeRandomProvider = CreateRuntimeRandomProvider(),
                RuntimeOrderProvider = CreateRuntimeOrderProvider(),
                RuntimeIdProvider = CreateRuntimeIdProvider(),
                EventPublisher = eventPublisher,
                EventSource = eventsSource,
                ContextFactory = this,
            };
            
            context.GameQueueCollector = CreateGameQueueCollector(context);
            context.ObjectEventProcessor = CreateObjectEventProcessor(context);
            context.ContextEventProcessor = CreateContextEventProcessor(context);
            context.CommandFactory = CreateCommandFactory(context);
            context.StatFactory = CreateStatFactory(context);
            context.PlayerFactory = CreatePlayerFactory(context);
            context.EffectFactory = CreateEffectFactory(context);
            context.ObjectFactory = CreateObjectFactory(context);
            context.WinConditionProcessor = CreateWinConditionProcessor(context);
            context.TurnProcessor = CreateTurnProcessor(context);
            context.CroupierProcessor = CreateCroupierProcessor(context);
            context.GameEventProcessor = CreateGameEventProcessor(context);
            context.CommandProcessor = CreateCommandProcessor(context);
            context.TimerFactory = CreateTimerFactory(context);
            context.RuntimeTimer = context.TimerFactory.Create();
            
            return context;
        }
        #endregion
    }
}