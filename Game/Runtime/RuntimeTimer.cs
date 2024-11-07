using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Runtime
{
    public class RuntimeTimer : IRuntimeTimer
    {
        public TimerConfig Config { get; private set; }
        public IRuntimeTimerModel RuntimeModel { get; private set; }
        public IEventPublisher EventPublisher { get; private set; }
        private IPlayersCollection playersCollection;

        public RuntimeTimer(
            TimerConfig config,
            IRuntimeTimerModel runtimeModel,
            IPlayersCollection playersCollection,
            IEventPublisher eventPublisher)
        {
            Config = config;
            EventPublisher = eventPublisher;
            this.playersCollection = playersCollection;
            Sync(runtimeModel);
        }

        public void Dispose()
        {
            Config = null;
            RuntimeModel = null;
            EventPublisher = null;
            playersCollection = null;
        }

        public IRuntimeTimer Sync(IRuntimeTimerModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            return this;
        }

        public void SetState(TimerState value, bool notify = true)
        {
            RuntimeModel.State = value;
            // TODO notify
            throw new NotImplementedException();
        }

        public void SetTurnOwner(string value, bool notify = true)
        {
            RuntimeModel.OwnerId = value;
            // TODO notify
            throw new NotImplementedException();
        }

        public void PassTurn(bool notify = true)
        {
            var nextPlayer = RuntimeModel.OwnerId == null
                ? playersCollection.First(x => x.RuntimeModel.IsFirst)
                : playersCollection.GetOpposite(RuntimeModel.OwnerId);
            
            RuntimeModel.Turn++;
            if (RuntimeModel.Round % 2 == 0)
                RuntimeModel.Round++;

            RuntimeModel.TimeLeft = GetNextTimer();
            SetTurnOwner(nextPlayer.RuntimeModel.OwnerId, notify);
        }

        private double GetNextTimer()
        {
            // TODO correct
            return RuntimeModel.State switch
            {
                TimerState.NotStarted => Config.RoundSec,
                TimerState.Mulligan => Config.MulliganSec,
                TimerState.Game => Config.RoundSec,
                TimerState.Paused => Config.RoundSec,
                TimerState.End => 0,
                _ => throw new NotImplementedException($"Unknown {nameof(TimerState)} : {RuntimeModel.State}.")
            };
        }
    }
}