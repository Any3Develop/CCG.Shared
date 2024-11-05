using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Game.Config;
using CCG.Shared.Game.Enums;
using CCG.Shared.Game.Runtime;
using CCG.Shared.Game.Runtime.Models;

namespace CCG.Shared.Game.Factories
{
    public class RuntimeTimerFactory : IRuntimeTimerFactory
    {
        private readonly ISharedConfig sharedConfig;
        private readonly IContext context;

        public RuntimeTimerFactory(
            ISharedConfig sharedConfig,
            IContext context)
        {
            this.sharedConfig = sharedConfig;
            this.context = context;
        }

        public IRuntimeTimerModel CreateModel(bool notify = false)
        {
            return CreateModel(0, null, null, notify);
        }
        
        public IRuntimeTimerModel CreateModel(int? runtimeId, string ownerId, string dataId, bool notify = true)
        {
            if (sharedConfig.Timer == null)
                throw new NullReferenceException($"{nameof(TimerConfig)} with id {dataId}, not found in {nameof(ISharedConfig)}");

            return new RuntimeTimerModel
            {
                OwnerId = ownerId,
                Turn = 0,
                Round = 0,
                TimeLeft = 0,
                State = TimerState.NotStarted
            };
        }

        public IRuntimeTimer Create(IRuntimeTimerModel runtimeModel, bool notify = true)
        {
            if (context.RuntimeTimer != null)
                return context.RuntimeTimer.Sync(runtimeModel);
            
            if (sharedConfig.Timer == null)
                throw new NullReferenceException($"{nameof(TimerConfig)} not found in {nameof(ISharedConfig)}");
            
            return new RuntimeTimer(sharedConfig.Timer, context.EventPublisher, context.EventSource).Sync(runtimeModel);
        }
    }
}