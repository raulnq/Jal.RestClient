using Jal.RestClient.Interface.Fluent;

namespace Jal.RestClient.Impl.Fluent
{
    public static class RestFluentDescriptorExtensions
    {
        public static IRestAuthenticatorDescriptor AuthorizedByBearerToken(this IRestAuthenticatorDescriptor descriptor, string token)
        {
            var auth = new TokenAuthenticator(token, "Bearer");
            descriptor.AuthorizedBy(auth.Authenticate);
            return descriptor;
        }

        public static IRestAuthenticatorDescriptor AuthorizedByToken(this IRestAuthenticatorDescriptor descriptor, string token, string type)
        {
            var auth = new TokenAuthenticator(token, type);
            descriptor.AuthorizedBy(auth.Authenticate);
            return descriptor;
        }

        public static IRestAuthenticatorDescriptor AuthorizedByBasicHttp(this IRestAuthenticatorDescriptor descriptor, string user, string password)
        {
            var auth = new BasicHttpAuthenticator(user, password);
            descriptor.AuthorizedBy(auth.Authenticate);
            return descriptor;
        }
    }
}
