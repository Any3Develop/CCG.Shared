using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
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
            EventsSource?.Dispose();
            StatsCollection?.Dispose();
        }

        public IRuntimePlayer Sync(IRuntimePlayerModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
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

        public void AddStat(IRuntimeStat stat, bool notify = true)
        {
            if (StatsCollection.Contains(stat.RuntimeModel.Id))
                throw new InvalidOperationException($"Can't add stat twice : {stat.RuntimeModel.ReflectionFormat()}");

            RuntimeModel.Stats.Add(stat.RuntimeModel);
            StatsCollection.Add(stat, notify);
        }

        public void RemoveStat(IRuntimeStat stat, bool notify = true)
        {
            if (!StatsCollection.Contains(stat.RuntimeModel.Id))
                return;

            RuntimeModel.Stats.RemoveAll(x => x.Id == stat.RuntimeModel.Id);
            StatsCollection.Remove(stat, notify);
            stat.Dispose();
        }
    }
}