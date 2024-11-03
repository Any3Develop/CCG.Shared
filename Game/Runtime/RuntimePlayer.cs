using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Events.Context.Players;
using CCG.Shared.Game.Utils;

namespace CCG.Shared.Game.Runtime
{
    public class RuntimePlayer : IRuntimePlayer
    {
        public PlayerConfig Config { get; private set; }
        public IRuntimePlayerModel RuntimeModel { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        protected bool Initialized { get; private set; }

        public RuntimePlayer(
            PlayerConfig config,
            IStatsCollection statsCollection, 
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Config = config;
            EventPublisher = eventPublisher;
            StatsCollection = statsCollection;
            EventsSource = eventsSource;
        }

        public void Dispose()
        {
            if (!Initialized)
                return;

            Initialized = false;
            EventsSource?.Dispose();
            StatsCollection?.Dispose();
        }

        public IRuntimePlayer Sync(IRuntimePlayerModel runtimeModel, bool notify = true)
        {
            Initialized = true;
            EventPublisher.Publish<BeforePlayerChangeEvent>(notify, this);
            RuntimeModel = runtimeModel;
            EventPublisher.Publish<AfterPlayerChangedEvent>(notify, this);
            return this;
        }

        public bool TrySpendMana(int value)
        {
            if (!Initialized || value < 0)
                return false;

            var runtimeStat = StatsCollection.First();
            if (runtimeStat == null)
                return false;

            var remainder = runtimeStat.RuntimeModel.Value - value;
            if (remainder < 0)
                return false;
            
            runtimeStat.SetValue(remainder);
            return true;
        }

        public void SetReady(bool value)
        {
            RuntimeModel.Ready = value;
        }
    }
}