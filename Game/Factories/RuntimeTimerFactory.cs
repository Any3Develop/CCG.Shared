using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Abstractions.Game.Context.Providers;
using CCG.Shared.Abstractions.Game.Factories;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;
using CCG.Shared.Common.Utils;
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

        public IRuntimeTimerModel CreateModel()
        {
            if (sharedConfig.Timer == null)
                throw new NullReferenceException($"{nameof(TimerConfig)} not found in {nameof(ISharedConfig)}");

            return new RuntimeTimerModel
            {
                Turn = 0,
                Round = 0,
                TimeLeftMs = 0,
                State = TimerState.NotStarted
            };
        }

        public IRuntimeTimer Create(IRuntimeTimerModel runtimeModel)
        {
            if (context.RuntimeTimer != null)
                throw new InvalidOperationException($"Unable create the timer twice : {runtimeModel.AsJsonFormat()}");

            if (sharedConfig.Timer == null)
                throw new NullReferenceException($"{nameof(TimerConfig)} not found in {nameof(ISharedConfig)}");

            return new RuntimeTimer(sharedConfig.Timer, runtimeModel, context.PlayersCollection, context.EventPublisher, context.SystemTimers);
        }
    }
}