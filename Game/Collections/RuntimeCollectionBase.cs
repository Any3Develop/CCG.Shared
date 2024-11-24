using System.Collections;
using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Abstractions.Game.Runtime;
using CCG.Shared.Abstractions.Game.Runtime.Models;

namespace CCG.Shared.Game.Collections
{
    public abstract class RuntimeCollectionBase<TRuntime> : IRuntimeCollection<TRuntime> where TRuntime : IRuntimeBase
    {
        protected readonly List<TRuntime> Runtimes = new();
        protected IList LinkedModels;
        
        public virtual int Count => Runtimes.Count;
        public virtual TRuntime this[int index] => Runtimes[index];

        public virtual void Dispose()
        {
            LinkedModels = null;
            Runtimes?.OfType<IDisposable>().ToList().ForEach(x => x?.Dispose());
            Clear();
        }

        public void LinkModelsList<TModel>(List<TModel> external) where TModel : IContextModel
        {
            LinkedModels = external;
        }

        public void Replace(TRuntime value)
        {
            if (Remove(value, false))
                Add(value, false);
        }

        public virtual bool Contains(TRuntime value)
        {
            return value != null && Contains(value.RuntimeModel.Id);
        }

        public bool Contains<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Runtimes.OfType<T>().Any(predicate.Invoke);
        }
        
        public bool Contains(int id)
        {
            return Runtimes.Any(x => x.RuntimeModel.Id == id);
        }
        
        public bool Contains(string ownerId)
        {
            return Runtimes.Any(x => x.RuntimeModel.OwnerId == ownerId);
        }

        public virtual void Sort(Comparison<TRuntime> comparison)
        {
            if (Runtimes.Count == 0)
                return;
            
            Runtimes.Sort(comparison);
            
            if (LinkedModels is null or {Count: 0})
                return;

            for (var currIndex = 0; currIndex < Runtimes.Count; currIndex++)
                InsertOrMoveLinked(currIndex, Runtimes[currIndex].RuntimeModel);
        }

        protected virtual void InsertOrMoveLinked(int newIndex, object model)
        {
            if (LinkedModels is null || newIndex < 0 || newIndex > LinkedModels.Count)
                return;
                
            LinkedModels.Remove(model);
            LinkedModels.Insert(newIndex, model);
        }

        public virtual void Clear()
        {
            Runtimes.Clear();
            LinkedModels?.Clear();
        }

        public virtual bool Insert(int index, TRuntime value, bool notify = true)
        {
            if (index < 0 || index > Count || value == null || Contains(value))
                return false;

            Runtimes.Insert(index, value);

            if (LinkedModels != null)
                InsertOrMoveLinked(index, value.RuntimeModel);
            
            if (notify)
                AddNotify(value);
            
            return true;
        }

        public virtual bool Add(TRuntime value, bool notify = true)
        {
            if (value == null || Contains(value))
                return false;

            Runtimes.Add(value);
            
            if (LinkedModels != null && !LinkedModels.Contains(value))
                LinkedModels.Add(value.RuntimeModel);
            
            if (notify)
                AddNotify(value);
            
            return true;
        }

        public virtual int AddRange(IEnumerable<TRuntime> values, bool notify = true)
        {
            return values?.Count(value => Add(value, notify)) ?? 0;
        }

        public virtual void AddNotify(TRuntime value){}

        public virtual bool Remove(int id, bool notify = true)
        {
            return Runtimes.Where(x => x.RuntimeModel.Id == id).ToArray().Aggregate(false, (current, value) => current | Remove(value, notify));
        }

        public virtual bool Remove(TRuntime value, bool notify = true)
        {
            var result = Runtimes.Remove(value);
            
            if (result && LinkedModels != null)
                LinkedModels.Remove(value.RuntimeModel);
            
            if (result && notify)
                RemoveNotify(value);
            
            return result;
        }

        public virtual void RemoveNotify(TRuntime value){}

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
            return Runtimes.Find(x => x.RuntimeModel.Id == id);
        }

        public T Get<T>(int id) where T : TRuntime
        {
            return (T)Runtimes.Find(x => x.RuntimeModel.Id == id && x is T);
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
            return Runtimes.OfType<T>().FirstOrDefault(predicate.Invoke);
        }

        public T GetLast<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Runtimes.OfType<T>().LastOrDefault(predicate.Invoke);
        }

        public virtual TRuntime GetFirst(Predicate<TRuntime> predicate)
        {
            return Runtimes.Find(predicate);
        }

        public virtual TRuntime GetLast(Predicate<TRuntime> predicate)
        {
            return Runtimes.FindLast(predicate);
        }

        public virtual TRuntime[] GetAll()
        {
            return Runtimes.ToArray();
        }

        public TRuntime[] GetAll(string ownerId)
        {
            return this.Where(x => x.RuntimeModel.OwnerId == ownerId).ToArray();
        }

        public T[] GetAll<T>() where T : TRuntime
        {
            return Runtimes.OfType<T>().ToArray();
        }

        public T[] GetAll<T>(string ownerId) where T : TRuntime
        {
            return this.OfType<T>().Where(x => x.RuntimeModel.OwnerId == ownerId).ToArray();
        }

        public virtual TRuntime[] GetRange(IEnumerable<int> ids)
        {
            return ids == null 
                ? Array.Empty<TRuntime>() 
                : Runtimes.Where(x => ids.Contains(x.RuntimeModel.Id)).ToArray();
        }

        public T[] GetRange<T>(IEnumerable<int> ids) where T : TRuntime
        {
            return ids == null 
                ? Array.Empty<T>() 
                : Runtimes.OfType<T>().Where(x => ids.Contains(x.RuntimeModel.Id)).ToArray();
        }

        public virtual TRuntime[] GetRange(Predicate<TRuntime> predicate)
        {
            return Runtimes.Where(predicate.Invoke).ToArray();
        }

        public T[] GetRange<T>(Predicate<T> predicate) where T : TRuntime
        {
            return Runtimes.OfType<T>().Where(predicate.Invoke).ToArray();
        }

        #region IEnumerable
        public IEnumerator<TRuntime> GetEnumerator() => Runtimes.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}