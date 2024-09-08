﻿using System.Collections;
using CCG_Shared.Abstractions.Game.Collections;

namespace CCG_Shared.Game.Collections
{
    public abstract class RuntimeCollectionBase<TRuntime> : IRuntimeCollection<TRuntime>
    {
        protected readonly List<TRuntime> Collection = new();
        public virtual int Count => Collection.Count;
        public virtual TRuntime this[int index] => Collection[index];

        protected abstract int GetId(TRuntime value);

        public virtual void Dispose()
        {
            Collection?.OfType<IDisposable>().ToList().ForEach(x => x?.Dispose());
            Clear();
        }
        
        public virtual bool Contains(TRuntime value)
        {
            return value != null
                   && Contains(GetId(value));
        }

        public bool Contains<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Collection.OfType<T>().Any(predicate.Invoke);
        }
        
        public virtual bool Contains(int id)
        {
            return Collection.Any(x => GetId(x) == id);
        }

        public virtual void Sort(Comparison<TRuntime> comparison) => Collection.Sort(comparison);

        public virtual void Clear() => Collection.Clear();

        public virtual bool Insert(int index, TRuntime value, bool notify = true)
        {
            if (index < 0 || index > Count || value == null || Contains(value))
                return false;

            Collection.Insert(index, value);
            return true;
        }

        public virtual bool Add(TRuntime value, bool notify = true)
        {
            if (value == null || Contains(value))
                return false;

            Collection.Add(value);
            return true;
        }

        public virtual int AddRange(IEnumerable<TRuntime> values, bool notify = true)
        {
            return values?.Count(value => Add(value, notify)) ?? 0;
        }

        public virtual bool Remove(int id, bool notify = true)
        {
            return Collection.RemoveAll(x => GetId(x) == id) > 0;
        }

        public virtual bool Remove(TRuntime value, bool notify = true)
        {
            return value != null && Remove(GetId(value), notify);
        }

        public virtual int RemoveRange(IEnumerable<TRuntime> values, bool notify = true)
        {
            return values?.Count(value => Remove(value, notify)) ?? 0;
        }

        public virtual int RemoveRange(IEnumerable<int> ids, bool notify = true)
        {
            return ids?.Count(value => Remove(value, notify)) ?? 0;
        }

        public virtual TRuntime Get(int id)
        {
            return Collection.FirstOrDefault(x => GetId(x) == id);
        }

        public T Get<T>(int id) where T : TRuntime
        {
            return Collection.OfType<T>().FirstOrDefault(x => GetId(x) == id);
        }

        public bool TryGet(int id, out TRuntime result)
        {
            result = Get(id);
            return result != null;
        }

        public bool TryGet<T>(int id, out T result) where T : TRuntime
        {
            result = Get<T>(id);
            return result != null;
        }

        public bool TryGet(Predicate<TRuntime> predicate, out TRuntime result)
        {
            result = GetFirst(predicate);
            return result != null;
        }

        public T GetFirst<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Collection.OfType<T>().FirstOrDefault(predicate.Invoke);
        }

        public T GetLast<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Collection.OfType<T>().LastOrDefault(predicate.Invoke);
        }

        public virtual TRuntime GetFirst(Predicate<TRuntime> predicate)
        {
            return Collection.Find(predicate);
        }

        public virtual TRuntime GetLast(Predicate<TRuntime> predicate)
        {
            return Collection.FindLast(predicate);
        }

        public virtual TRuntime[] GetAll()
        {
            return Collection.ToArray();
        }

        public T[] GetAll<T>() where T : TRuntime
        {
            return Collection.OfType<T>().ToArray();
        }

        public virtual TRuntime[] GetRange(IEnumerable<int> ids)
        {
            return ids == null 
                ? Array.Empty<TRuntime>() 
                : Collection.Where(x => ids.Contains(GetId(x))).ToArray();
        }

        public T[] GetRange<T>(IEnumerable<int> ids) where T : TRuntime
        {
            return ids == null 
                ? Array.Empty<T>() 
                : Collection.OfType<T>().Where(x => ids.Contains(GetId(x))).ToArray();
        }

        public virtual TRuntime[] GetRange(Predicate<TRuntime> predicate)
        {
            return Collection.Where(predicate.Invoke).ToArray();
        }

        public T[] GetRange<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Collection.OfType<T>().Where(predicate.Invoke).ToArray();
        }

        #region IEnumerable
        public IEnumerator<TRuntime> GetEnumerator() => Collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}