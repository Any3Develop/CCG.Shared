using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Game.Config
{
    public class TimerConfig : IConfig
    {
        public string Id => nameof(TimerConfig);
        public int RoundMs { get; private set; }
        public int MulliganMs { get; private set; }
    }
}