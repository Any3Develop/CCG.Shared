using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Game.Context
{
    public class SharedConfig : ISharedConfig
    {
        public int MaxInTableCount { get; set; }
        public int MaxInHandCount { get; set; }
        public int MaxInDeckCount { get; set; }
    }
}