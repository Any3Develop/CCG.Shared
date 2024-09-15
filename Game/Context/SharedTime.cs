using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Game.Context
{
    public class SharedTime : ISharedTime
    {
        public DateTime Current => DateTime.UtcNow;
    }
}