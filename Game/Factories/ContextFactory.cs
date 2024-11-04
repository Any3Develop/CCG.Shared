using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventProcessors;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Game.Collections;
using CCG.Shared.Game.Commands.Base;
using CCG.Shared.Game.Context;
using CCG.Shared.Game.Context.EventProcessors;
using CCG.Shared.Game.Context.Providers;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime.Effects;

namespace CCG.Shared.Game.Factories
{
    public class ContextFactory : IContextFactory
    {
        private readonly IDatabase database;
        private readonly ISharedConfig sharedConfig;
        private readonly IEventsSourceFactory eventsSourceFactory;
        private readonly ITypeCollection<LogicId, RuntimeEffectBase> logicTypeCollection;
        private readonly ITypeCollection<string, Command> commandTypeCollection;

        public ContextFactory(
            IDatabase database,
            ISharedConfig sharedConfig,
            IEventsSourceFactory eventsSourceFactory,
            ITypeCollection<LogicId, RuntimeEffectBase> logicTypeCollection, 
            ITypeCollection<string, Command> commandTypeCollection)
        {
            this.database = database;
            this.sharedConfig = sharedConfig;
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
                sharedConfig,
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
                sharedConfig, 
                GetRequiredArgument<IContext>());
        }

        #endregion

        #region Common

        private T GetRequiredArgument<T>(params object[] args)
        {
            return args.OfType<T>().FirstOrDefault();
        }

        private IEnumerable<T> GetRequiredArguments<T>(params object[] args)
        {
            return args.OfType<T>().ToArray();
        }

        #endregion
    }
}