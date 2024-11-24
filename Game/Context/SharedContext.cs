using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Context.Processors;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Context
{
    public class SharedContext : IContext
    {
        #region Static Context
        public ISystemTimers SystemTimers { get; set; }
        public ISharedTime SharedTime { get; set; }
        public ISharedConfig Config { get; set; }
        public IDatabase Database { get; set; }
        #endregion

        #region Runtime Context
        public IRuntimeContextModel RuntimeData { get; private set; }
        public IRuntimeTimer RuntimeTimer { get; set; }
        public IObjectsCollection ObjectsCollection { get; set; }
        public IPlayersCollection PlayersCollection { get; set; }
        public IRuntimeRandomProvider RuntimeRandomProvider { get; set; }
        public IRuntimeOrderProvider RuntimeOrderProvider { get; set; }
        public IRuntimeIdProvider RuntimeIdProvider { get; set; }

        #endregion

        #region Logic Context
        
        public ITurnProcessor TurnProcessor { get; set; }
        public ICroupierProcessor CroupierProcessor { get; set; }
        public IObjectEventProcessor ObjectEventProcessor { get; set; }
        public IContextEventProcessor ContextEventProcessor { get; set; }
        public IGameEventProcessor GameEventProcessor { get; set; }
        public ICommandProcessor CommandProcessor { get; set; }
        public IGameQueueCollector GameQueueCollector { get; set; }
        public IEventPublisher EventPublisher { get; set; }
        public IEventsSource EventSource { get; set; }
        public ICommandFactory CommandFactory { get; set; }
        public IRuntimeObjectFactory ObjectFactory { get; set; }
        public IRuntimePlayerFactory PlayerFactory { get; set; }
        public IRuntimeEffectFactory EffectFactory { get; set; }
        public IRuntimeStatFactory StatFactory { get; set; }
        public IRuntimeTimerFactory TimerFactory { get; set; }
        public IContextFactory ContextFactory { get; set; }
        public IWinConditionProcessor WinConditionProcessor { get; set; }

        #endregion

        public IContext Sync(IRuntimeContextModel value)
        {
            RuntimeData = value;
            return this;
        }

        public void Start()
        {
            if (RuntimeData.IsStarted)
                throw new InvalidOperationException("Can't start game twice.");
            
            RuntimeData.StartTime = SharedTime.Current;
        }

        public void Ready()
        {
            if (RuntimeData.IsReady)
                throw new InvalidOperationException("Can't ready game twice.");
            
            RuntimeData.ReadyTime = SharedTime.Current;
        }

        public void End()
        {
            if (RuntimeData.IsEnded)
                throw new InvalidOperationException("Can't end game twice.");
            
            RuntimeData.EndTime = SharedTime.Current;
            EventSource.Dispose();
        }
    }
}