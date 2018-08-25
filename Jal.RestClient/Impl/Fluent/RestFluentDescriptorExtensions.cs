using System;
using Jal.RestClient.Interface.Fluent;

namespace Jal.RestClient.Impl.Fluent
{
    public static class RestFluentDescriptorExtensions
    {
        public static IRestHeaderDescriptor AuthorizedByBearerToken(this IRestMiddlewareDescriptor descriptor, string token)
        {
            descriptor.WithContext(x => { x.Add("token", token); x.Add("tokentype", "Bearer"); });
            return descriptor.WithMiddleware(x => x.Add<TokenAuthenticatorHttpMiddleware>());
        }


        public static IRestHeaderDescriptor AuthorizedByToken(this IRestMiddlewareDescriptor descriptor, string token, string type)
        {
            descriptor.WithContext(x => { x.Add("token", token); x.Add("tokentype", type); });
            return descriptor.WithMiddleware(x => x.Add<TokenAuthenticatorHttpMiddleware>());
        }

        public static IRestHeaderDescriptor AuthorizedByBasicHttp(this IRestMiddlewareDescriptor descriptor, string user, string password)
        {
            descriptor.WithContext(x => { x.Add("user", user); x.Add("password", password); });
            return descriptor.WithMiddleware(x => x.Add<BasicHttpAuthenticatorHttpMiddleware>());
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
