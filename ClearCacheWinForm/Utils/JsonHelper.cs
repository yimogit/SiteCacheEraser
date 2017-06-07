using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClearCacheWinForm.Utils
{
    public class JsonHelper
    {
        public static string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static string SerializeObject(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
        }

        public static T DeserializeObject<T>(string input) where T : class
        {
            return JsonConvert.DeserializeObject<T>(input, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
        }
    }
}
