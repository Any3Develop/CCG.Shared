using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Utils.Disposables;

namespace CCG.Shared.Game.Runtime
{
    public class RuntimePlayer : IRuntimePlayer
    {
        public PlayerConfig Config { get; private set; }
        public IRuntimePlayerModel RuntimeModel { get; private set; }
        public IStatsCollection StatsCollection { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        private IDisposables disposables;
        
        public RuntimePlayer(
            PlayerConfig config,
            IRuntimePlayerModel runtimeModel,
            IStatsCollection statsCollection, 
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Config = config;
            EventPublisher = eventPublisher;
            StatsCollection = statsCollection;
            EventsSource = eventsSource;
            EventsSource.AddTo(ref disposables);
            StatsCollection.AddTo(disposables);
            Sync(runtimeModel);
        }

        public void Dispose()
        {
            disposables?.Dispose();
            Config = null;
            disposables = null;
            RuntimeModel = null;
            StatsCollection = null;
            EventPublisher = null;
            EventsSource = null;
        }

        public IRuntimePlayer Sync(IRuntimePlayerModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            StatsCollection.LinkModelCollection(RuntimeModel.Stats);
            return this;
        }

        public bool TrySpendMana(int value)
        {
            if (value < 0)
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

        #region IRuntimeObjectBase
        IRuntimeModelBase IRuntimeObjectBase.RuntimeModel => RuntimeModel;
        IConfig IRuntimeObjectBase.Config => Config;
        #endregion
    }
}