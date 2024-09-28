using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Game.Config
{
    public class TimerConfig : IConfig
    {
        public string Id => nameof(TimerConfig);
        public double RoundSec { get; private set; }
        public double MulliganSec { get; private set; }
        public double DelayedClientsMs { get; private set; }
    }
}