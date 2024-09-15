using CCG.Shared.Abstractions.Game.Context;

namespace CCG.Shared.Abstractions.Game.Collections
{
    public interface IConfigCollection<TConfig> : IList<TConfig> where TConfig : IConfig
    {
        TConfig Get(string id);
        T Get<T>(string id) where T : TConfig;
        bool TryGet(string id, out TConfig result);
        bool TryGet<T>(string id, out T result) where T : TConfig;
        void AddRange(IEnumerable<TConfig> values);

        IEnumerable<TConfig> GetRange(IEnumerable<string> ids);
        IEnumerable<T> GetRange<T>(IEnumerable<string> ids) where T : TConfig;
    }
}