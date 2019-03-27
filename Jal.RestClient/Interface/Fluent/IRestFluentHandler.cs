using System;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestFluentHandler
    {
        IRestMiddlewareDescriptor Uri(string uri);

        IRestMiddlewareDescriptor Uri(string uri, System.Net.Http.HttpClient client);

        IRestMiddlewareDescriptor Uri(string uri, Func<System.Net.Http.HttpClient> factory);
    }
}