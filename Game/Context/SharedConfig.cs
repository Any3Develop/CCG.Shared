using CCG_Shared.Abstractions.Game.Context;

namespace CCG_Shared.Game.Context
{
    public class SharedConfig : ISharedConfig
    {
        public int MaxInTableCount { get; set; }
        public int MaxInHandCount { get; set; }
        public int MaxInDeckCount { get; set; }
    }
}