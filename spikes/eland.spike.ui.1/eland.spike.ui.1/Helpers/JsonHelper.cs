using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace SilverlightSpike1.Helpers
{
    public class JsonHelper<T>
    {
        public static T ConvertJsonStringToObject(string json)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var bytes = Encoding.Unicode.GetBytes(json);
                    memoryStream.Write(bytes, 0, bytes.Length);
                    memoryStream.Position = 0;

                    return ConvertJsonStringToObject(memoryStream);
                }
            }
            catch (SerializationException)
            {
                return default(T);
            }
        }

        public static T ConvertJsonStringToObject(Stream json)
        {
            try
            {
                var dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
                return (T)dataContractJsonSerializer.ReadObject(json);
            }
            catch (SerializationException)
            {
                return default(T);
            }
        }
    }
}
