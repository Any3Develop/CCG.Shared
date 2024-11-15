using CCG.Shared.Abstractions.Game.Context;
using CCG.Shared.Game.Enums;

namespace CCG.Shared.Game.Config
{
    public class StatConfig : IConfig
    {
        public StatType Type { get; private set; }
        public string Id { get; private set; }
        public int Value { get; private set; }
        public int Max { get; private set; }
    }
}