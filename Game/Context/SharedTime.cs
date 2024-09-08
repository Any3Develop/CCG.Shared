using CCG_Shared.Abstractions.Game.Context;

namespace CCG_Shared.Game.Context
{
    public class SharedTime : ISharedTime
    {
        public DateTime Current => DateTime.UtcNow;
    }
}