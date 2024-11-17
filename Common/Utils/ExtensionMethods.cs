using System.Collections;
using System.Reflection;
using CCG.Shared.Common.Logger;
using Newtonsoft.Json;

namespace CCG.Shared.Common.Utils
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Fills the T instance with data from an object with the same field or property names. Preserves default or deep copy references.
        /// </summary>
        /// <param name="destination">Target entity to populate the data from the object.</param>
        /// <param name="from">Reference Data Object.</param>
        /// <param name="deepCopy">Should apply a deep copy of the data from the object to the destination?</param>
        /// <exception cref="NullReferenceException">The 'destination' or 'from' object is null.</exception>
        public static T Map<T>(this T destination, object from, bool deepCopy = false)
        {
            if (destination == null)
                throw new NullReferenceException($"ExtensionMethods.{nameof(Map)} argument : {nameof(destination)} with type : {typeof(T).Name} - is null");

            if (from == null)
                throw new NullReferenceException($"ExtensionMethods.{nameof(Map)} argument : {nameof(from)} - is null");

            var fromType = from.GetType();
            var distType = destination.GetType();
            from = deepCopy ? from.DeepCopy() : from;
            
            foreach (var distField in distType.GetFields())
            {
                var fromField = fromType.GetField(distField.Name);
                if (fromField != null && distField.FieldType == fromField.FieldType)
                    distField.SetValue(destination, fromField.GetValue(from));
            }

            foreach (var distPropetry in distType.GetProperties().Where(x => x.CanWrite))
            {
                var fromProperty = distType.GetProperty(distPropetry.Name);
                if (fromProperty is {CanRead: true} && distPropetry.PropertyType == fromProperty.PropertyType)
                    distPropetry.SetValue(destination, fromProperty.GetValue(from));
            }

            return destination;
        }
        
        /// <summary>
        /// Serializes an object to json and then restores it's type as a new instance with a copy of the data.
        /// </summary>
        public static T DeepCopy<T>(this T instance) where T : class
        {
            if (instance is null or string)
                return instance;

            var jsonData = JsonConvert.SerializeObject(instance, SerializeExtensions.SerializeSettings);
            return JsonConvert.DeserializeObject<T>(jsonData, SerializeExtensions.GetDeserializeSettingsByType<T>());
        }
        /// <summary>
        /// Comparing objects based on their values. If objects are reference or array objects - recursively to compare all values.
        /// </summary>
        public static bool CompareValues<T>(this T value1, T value2)
        {
            try
            {
                if (value1 == null && value2 == null)
                    return true;
                
                if (value1 == null || value2 == null)
                    return false;
                
                var type = value1.GetType();
                const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
                
                return type.GetFields(flags).All(x => Compare(x.GetValue(value1), x.GetValue(value2))) 
                       && type.GetProperties(flags).All(x => Compare(x.GetValue(value1), x.GetValue(value2)));
            }
            catch (Exception e)
            {
                SharedLogger.Error(e);
                return false;
            }
        }
        
        private static bool Compare(object value1, object value2)
        {
            if (value1 == null && value2 == null)
                return true;
                
            if (value1 == null || value2 == null)
                return false;
            
            if (value1 is IEnumerable collection1 && value2 is IEnumerable collection2)
                return Compare(collection1, collection2);

            return value1.GetType().IsValueType || value1 is string
                ? value1.Equals(value2) 
                : CompareValues(value1, value2);
        }

        private static bool Compare(IEnumerable value1, IEnumerable value2)
        {
            if (value1 == null && value2 == null)
                return true;
                
            if (value1 == null || value2 == null)
                return false;

            var enumerator1 = value1.GetEnumerator();
            var enumerator2 = value2.GetEnumerator();
            
            while (true)
            {
                var move1 = enumerator1.MoveNext();
                var move2 = enumerator2.MoveNext();
                
                if (!move1 && !move2)
                    return true;
                
                if (move1 != move2)
                    return false;
                
                if (!CompareValues(enumerator1.Current, enumerator2.Current))
                    return false;
            }
        }
    }
}