using CCG.Shared.Abstractions.Game.Context.Providers;

namespace CCG.Shared.Game.Context.Providers
{
    public class SharedTime : ISharedTime
    {
        public DateTime Current => DateTime.UtcNow;
    }
}