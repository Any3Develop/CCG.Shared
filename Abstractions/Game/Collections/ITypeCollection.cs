namespace CCG.Shared.Abstractions.Game.Collections
{
    public interface ITypeCollection<in TKey, TBase>
    {
        Type Get(TKey key);
        bool TryGet(TKey key, out Type result);
    }
}