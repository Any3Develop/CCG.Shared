namespace CCG.Shared.Abstractions.Game.Context.Providers
{
    public interface ISharedTime
    {
        DateTime Current { get; }
    }
}