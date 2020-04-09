using System;
using System.Threading.Tasks;
using Jal.HttpClient;

namespace Jal.RestClient
{
    public static class RestFluentDescriptorExtensions
    { 
        public static IRestSenderDescriptor WithTracing(this IRestSenderDescriptor descriptor, string requestid, string parentid = null, string operationid = null)
        {
            return descriptor.WithTracing(new HttpTracingContext(requestid, parentid, operationid));
        }

        public static IRestSenderDescriptor<T> WithTracing<T>(this IRestSenderDescriptor<T> descriptor, string requestid, string parentid = null, string operationid = null)
        {
            return descriptor.WithTracing(new HttpTracingContext(requestid, parentid, operationid));
        }

        public static IRestMapDescriptor FormUrlEncodedData(this IRestContentDescriptor descriptor, string data)
        {
            return descriptor.Data(data, "application/x-www-form-urlencoded");
        }

        public static IRestMapDescriptor MultipartFormData(this IRestContentDescriptor descriptor, string data)
        {
            return descriptor.Data(data, "multipart/form-data");
        }

        public static IRestMapDescriptor XmlData(this IRestContentDescriptor descriptor, string data)
        {
            return descriptor.Data(data, "text/xml");
        }

        public static IRestMapDescriptor XmlData<T>(this IRestContentDescriptor descriptor, T data, Func<T, string> converter)
        {
            return descriptor.Data(data, converter, "text/xml");
        }

        public static IRestWhenDescriptor<T> MapXmlTo<T>(this IRestMapDescriptor descriptor, Func<string, T> converter)
        {
            return descriptor.MapTo(converter, "text/xml");
        }
    }
}
