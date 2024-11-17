using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CCG.Shared.Common.Utils
{
    public interface ISharedSerializationBinder : ISerializationBinder
    {
        string DefaultAssemblyName { get; }
        string TargetAssemblyName { get; }
    }

    public class SharedSerializationBinder : ISharedSerializationBinder
    {
        public string DefaultAssemblyName => "mscorlib";
        public string TargetAssemblyName => "CCG.Shared";

        public Type BindToType(string assemblyName, string typeName)
        {
            if (assemblyName != TargetAssemblyName)
                assemblyName = DefaultAssemblyName;

            return Type.GetType($"{typeName}, {assemblyName}", true);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = serializedType.Assembly.FullName;
            typeName = serializedType.FullName;
        }
    }
    
    public static class SerializeExtensions
    {
        private static ISharedSerializationBinder SerializationBinder { get; } = new SharedSerializationBinder();

        public static JsonSerializerSettings DeserializeSettings { get; } = new()
        {
            TypeNameHandling = TypeNameHandling.Objects,
            SerializationBinder = SerializationBinder
        };

        public static JsonSerializerSettings SerializeSettings { get; } = new()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        public static JsonSerializerSettings GetDeserializeSettingsByType<T>()
        {
            return GetDeserializeSettingsByType(typeof(T));
        }

        public static JsonSerializerSettings GetDeserializeSettingsByType(Type targetType)
        {
            if (targetType.IsGenericParameter)
                targetType = targetType.GetGenericTypeDefinition();

            return IsSupportsWithSerializeSettings(targetType) ? DeserializeSettings : default;
        }

        public static bool IsSupportsWithSerializeSettings(Type type)
        {
            return type?.Assembly.FullName.Contains(SerializationBinder.TargetAssemblyName) ?? false;
        }
    }
}