using System.Text;
using CCG_Shared.Abstractions.Common.Network;
using CCG_Shared.Game.Utils;
using Newtonsoft.Json;

namespace CCG_Shared.Common.Network.Realizations
{
    public class NetworkSerializer : INetworkSerializer
    {
        public byte[] Serialize(object data)
        {
            var jsonData = data as string ?? JsonConvert.SerializeObject(data, SerializeExtensions.SerializeSettings);
            return Encoding.UTF8.GetBytes(jsonData);
        }

        public T Deserialize<T>(byte[] data)
        {
            var jsonData = GetJsonFromBytes(data);
            return JsonConvert.DeserializeObject<T>(jsonData, SerializeExtensions.GetDeserializeSettingsByType<T>());
        }

        public object Deserialize(byte[] data)
        {
            var jsonData = GetJsonFromBytes(data);
            return JsonConvert.DeserializeObject(jsonData, SerializeExtensions.DeserializeSettings);
        }

        private static string GetJsonFromBytes(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}