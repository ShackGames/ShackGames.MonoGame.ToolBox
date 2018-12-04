using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ShackGames.MonoGame.Toolbox.Providers.Impl
{
    public sealed class JsonSerializationProviderDefault : IJsonSerializationProvider
    {
        public T Deserialize<T>(string json) where T : class
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }

        public string Serialize<T>(T obj)
        {
            string json;

            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(stream, obj);
                var jsonBytes = stream.ToArray();

                json = Encoding.UTF8.GetString(jsonBytes, 0, jsonBytes.Length);
            }

            return json;
        }
    }
}
