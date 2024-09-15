using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Game.Config
{
    public class StatConfig : IConfig
    {
        public string Id { get; set; }
        public int Value { get; set; }
        public int Max { get; set; }
    }
}