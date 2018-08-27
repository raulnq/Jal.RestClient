using System;
using Jal.HttpClient.Interface.Fluent;
using Jal.RestClient.Interface.Fluent;

namespace Jal.RestClient.Impl.Fluent
{
    public static class RestFluentDescriptorExtensions
    {
        public static void AuthorizedByBearerToken(this IHttpMiddlewareDescriptor descriptor, string tokenvalue)
        {
            descriptor.Add<TokenAuthenticatorHttpMiddleware>(y => { y.Add("tokenvalue", tokenvalue); y.Add("tokentype", "Bearer"); });
        }


        public static void AuthorizedByToken(this IHttpMiddlewareDescriptor descriptor, string tokenvalue, string tokentype)
        {
            descriptor.Add<TokenAuthenticatorHttpMiddleware>(y => { y.Add("tokenvalue", tokenvalue); y.Add("tokentype", tokentype); });
        }

        public static void AuthorizedByBasicHttp(this IHttpMiddlewareDescriptor descriptor, string username, string password)
        {
            descriptor.Add<BasicHttpAuthenticatorHttpMiddleware>(y => { y.Add("username", username); y.Add("password", password); });
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
