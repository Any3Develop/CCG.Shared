using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Game.Config
{
    public class StatConfig : IConfig
    {
        public string Id { get; private set; }
        public int Value { get; private set; }
        public int Max { get; private set; }
    }
}