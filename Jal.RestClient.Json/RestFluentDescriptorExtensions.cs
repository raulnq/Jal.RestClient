using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jal.RestClient.Json
{
    public static class RestFluentDescriptorExtensions
    {
        public static IRestMapDescriptor Data<T>(this IRestContentDescriptor descriptor, T body)
        {
            return descriptor.Data<T>(body, Convert<T>);
        }

        public static IRestWhenDescriptor<T> MapTo<T>(this IRestMapDescriptor descriptor)
        {
            return descriptor.MapTo<T>(Convert<T>);
        }

        public static string Convert<T>(T request)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include, ContractResolver = new DefaultContractResolver() };

            return JsonConvert.SerializeObject(request, Formatting.None, jsonSerializerSettings);
        }

        public static T Convert<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
