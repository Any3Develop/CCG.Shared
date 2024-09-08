﻿using CCG_Shared.Abstractions.Game.Data;

namespace CCG_Shared.Abstractions.Game.Collections
{
    public interface IDataCollection<TData> : IList<TData> where TData : IData
    {
        TData Get(string id);
        T Get<T>(string id) where T : TData;
        bool TryGet(string id, out TData result);
        bool TryGet<T>(string id, out T result) where T : TData;
        void AddRange(IEnumerable<TData> values);

        IEnumerable<TData> GetRange(IEnumerable<string> ids);
        IEnumerable<T> GetRange<T>(IEnumerable<string> ids) where T : TData;
    }
}