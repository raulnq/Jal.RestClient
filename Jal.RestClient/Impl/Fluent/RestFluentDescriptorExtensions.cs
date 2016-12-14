using System;
using Jal.RestClient.Interface.Fluent;

namespace Jal.RestClient.Impl.Fluent
{
    public static class RestFluentDescriptorExtensions
    {
        public static IRestHeaderDescriptor AuthorizedByBearerToken(this IRestAuthenticatorDescriptor descriptor, string token)
        {
            var auth = new TokenAuthenticator(token, "Bearer");

            return descriptor.AuthorizedBy(auth.Authenticate);
        }

        public static IRestHeaderDescriptor AuthorizedByToken(this IRestAuthenticatorDescriptor descriptor, string token, string type)
        {
            var auth = new TokenAuthenticator(token, type);

            return descriptor.AuthorizedBy(auth.Authenticate);
        }

        public static IRestHeaderDescriptor AuthorizedByBasicHttp(this IRestAuthenticatorDescriptor descriptor, string user, string password)
        {
            var auth = new BasicHttpAuthenticator(user, password);

            return descriptor.AuthorizedBy(auth.Authenticate);
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
