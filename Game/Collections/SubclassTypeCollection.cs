using System.Reflection;
using CCG.Shared.Abstractions.Game.Collections;
using CCG.Shared.Common.Logger;

namespace CCG.Shared.Game.Collections
{
    /// <summary>
    /// Use it only as project singleton to prevent iteration whole assembly every scope creation.
    /// </summary>
    public class SubclassTypeCollection<TKey, TBase> : ITypeCollection<TKey, TBase>
    {
        private readonly Dictionary<TKey, Type> collection = new();
        
        public void Collect()
        {
            collection.Clear();
            foreach (var type in GetSubclassTypes())
            {
                var key = GetKeyFromType(type);
                if (collection.ContainsKey(key))
                {
                    SharedLogger.Error($"Key : {key} already exist in collection, type : {type}");
                    continue;
                }

                collection.Add(key, type);
            }
        }
        
        public void Collect(Func<Type, TKey> keySelector)
        {
            collection.Clear();
            foreach (var type in GetSubclassTypes(true))
            {
                var key = keySelector.Invoke(type);
                if (collection.ContainsKey(key))
                {
                    SharedLogger.Error($"Key : {key} already exist in collection, type : {type}");
                    continue;
                }

                collection.Add(key, type);
            }
        }
        
        public void Collect<TAttribute>(Func<TAttribute, TKey> keySelector) where TAttribute : Attribute
        {
            collection.Clear();
            foreach (var type in GetSubclassTypes(true))
            {
                foreach (var attribute in type.GetCustomAttributes<TAttribute>(false))
                {
                    var key = keySelector.Invoke(attribute);
                    if (collection.ContainsKey(key))
                    {
                        SharedLogger.Error($"Key : {key} already exist in collection, type : {type}");
                        continue;
                    }

                    collection.Add(key, type);
                }
            }
        }

        private static TKey GetKeyFromType(Type type)
        {
            if (type == typeof(string))
                return (TKey)(object)type.Name;

            return (TKey)(object)type;
        }

        private static IEnumerable<Type> GetSubclassTypes(bool includeAttributes = false)
        {
            var baseType = typeof(TBase);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly =>
                {
                    try
                    {
                        return assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException e)
                    {
                        SharedLogger.Error(e);
                        return e.Types.Where(type => type != null);
                    }
                })
                .Where(x => x.IsClass
                            && !x.IsAbstract
                            && x.IsSubclassOf(baseType)
                            && (!includeAttributes || x.CustomAttributes.Any()));
        }

        public virtual Type Get(TKey key)
        {
            return collection[key];
        }

        public virtual bool TryGet(TKey key, out Type result)
        {
            return collection.TryGetValue(key, out result);
        }
    }
}