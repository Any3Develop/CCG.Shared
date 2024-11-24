using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.EventSource;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Common.Utils;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Events.Context.Stats;

namespace CCG.Shared.Game.Runtime
{
    public class RuntimeStat : IRuntimeStat
    {
        public StatConfig Config { get; private set; }
        public IRuntimeStatModel RuntimeModel { get; private set; }
        public int Current => RuntimeModel?.Value ?? default;
        public int Max => RuntimeModel?.Max ?? default;
        public IEventPublisher EventPublisher { get; private set; }
        public IEventsSource EventsSource { get; private set; }

        public RuntimeStat(
            StatConfig config,
            IRuntimeStatModel runtimeModel,
            IEventPublisher eventPublisher,
            IEventsSource eventsSource)
        {
            Config = config;
            EventPublisher = eventPublisher;
            EventsSource = eventsSource;
            Sync(runtimeModel);
        }

        public virtual void Dispose()
        {
            Config = null;
            RuntimeModel = null;
            EventPublisher = null;
            EventsSource = null;
        }

        public IRuntimeStat Sync(IRuntimeStatModel runtimeModel)
        {
            RuntimeModel = runtimeModel;
            return this;
        }

        public void Override(int value, int max, bool notify = true)
        {
            OnBeforeChanged(notify);
            SetMax(max, false);
            SetValue(value, false);
            OnAfterChanged(notify);
        }

        public void RiseToMax(bool notify = true)
        {
            SetValue(RuntimeModel.Max, notify);
        }

        public virtual void SetValue(int value, bool notify = true)
        {
            OnBeforeChanged(notify);
            RuntimeModel.Value = Math.Min(value, RuntimeModel.Max);
            OnAfterChanged(notify);
        }

        public void Add(int value, bool notify = true)
        {
            if (value < 0)
                throw new InvalidOperationException($"Can't add less than zero [{value}] to the stat : {RuntimeModel.AsJsonFormat()}");
            
            OnBeforeChanged(notify);
            RuntimeModel.Value += value;
            OnAfterChanged(notify);
        }

        public void Subtract(int value, bool notify = true)
        {
            if (value < 0)
                throw new InvalidOperationException($"Can't subtract less than zero [{value}] to the stat : {RuntimeModel.AsJsonFormat()}");
            
            OnBeforeChanged(notify);
            RuntimeModel.Value -= value;
            OnAfterChanged(notify);
        }

        public virtual void SetMax(int value, bool notify = true)
        {
            OnBeforeChanged(notify);
            RuntimeModel.Max = value;
            SetValue(RuntimeModel.Value, false);
            OnAfterChanged(notify);
        }

        public virtual void Reset(bool notify = true)
        {
            OnBeforeChanged(notify);
            RuntimeModel.Max = Config.Max;
            RuntimeModel.Value = Config.Value;
            OnAfterChanged(notify);
        }

        #region Callbacks

        protected virtual void OnBeforeChanged(bool notify = true)
        {
            if (notify)
                EventPublisher.Publish(new BeforeStatChangeEvent(this));
        }

        protected virtual void OnAfterChanged(bool notify = true)
        {
            if (notify)
                EventPublisher.Publish(new AfterStatChangedEvent(this));
        }

        #endregion

        #region IRuntimeObjectBase

        IRuntimeBaseModel IRuntimeBase.RuntimeModel => RuntimeModel;

        IConfig IRuntimeBase.Config => Config;

        #endregion
    }
}