using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Jal.RestClient.Json
{
    public static class RestFluentDescriptorExtensions
    {
        public static IRestMapDescriptor Data<T>(this IRestContentDescriptor descriptor, T body, Action<JsonSerializerSettings> action=null)
        {
            return descriptor.Data<T>(body, t=>Convert<T>(t, action));
        }

        public static IRestWhenDescriptor<T> MapTo<T>(this IRestMapDescriptor descriptor)
        {
            return descriptor.MapTo<T>(Convert<T>);
        }

        public static string Convert<T>(T request, Action<JsonSerializerSettings> action = null)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include, ContractResolver = new DefaultContractResolver() };

            if(action!=null)
            {
                action(jsonSerializerSettings);
            }

            return JsonConvert.SerializeObject(request, Formatting.None, jsonSerializerSettings);
        }

        public static T Convert<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
