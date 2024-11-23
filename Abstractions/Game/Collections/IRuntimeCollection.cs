using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Abstractions.Game.Collections
{
    public interface IRuntimeCollection<TRuntime> : IDisposable, IEnumerable<TRuntime>
    {
        int Count { get; }
        TRuntime this[int index] { get; }
        void LinkModelsList<TModel>(List<TModel> external) where TModel : IContextModel;
        void Replace(TRuntime value);
        
        bool Contains(TRuntime value);
        bool Contains<T>(Predicate<T> predicate) where T : TRuntime;
        bool Contains(int id);
        bool Contains(string ownerId);
        void Sort(Comparison<TRuntime> comparison);
        void Clear();

        bool Insert(int index, TRuntime value, bool notify = true);
        
        bool Add(TRuntime value, bool notify = true);
        int AddRange(IEnumerable<TRuntime> values, bool notify = true);
        void AddNotify(TRuntime value);
        
        bool Remove(int id, bool notify = true);
        bool Remove(TRuntime value, bool notify = true);
        void RemoveNotify(TRuntime value);
        int RemoveRange(IEnumerable<TRuntime> values, bool notify = true);
        int RemoveRange(IEnumerable<int> ids, bool notify = true);

        TRuntime Get(int id);
        T Get<T>(int id) where T : TRuntime;
        bool TryGet(int id, out TRuntime result);
        bool TryGet<T>(int id, out T result) where T : TRuntime;
        bool TryGet(Predicate<TRuntime> predicate, out TRuntime result);
        T GetFirst<T>(Predicate<T> predicate) where T : TRuntime;
        T GetLast<T>(Predicate<T> predicate) where T : TRuntime;

        TRuntime[] GetAll();
        TRuntime[] GetAll(string ownerId);
        T[] GetAll<T>() where T : TRuntime;
        T[] GetAll<T>(string ownerId) where T : TRuntime;
        TRuntime[] GetRange(IEnumerable<int> ids);
        T[] GetRange<T>(IEnumerable<int> ids) where T : TRuntime;
        TRuntime[] GetRange(Predicate<TRuntime> predicate);
        T[] GetRange<T>(Predicate<T> predicate) where T : TRuntime;
    }
}