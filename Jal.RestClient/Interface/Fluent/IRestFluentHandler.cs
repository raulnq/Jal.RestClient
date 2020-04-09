using System;
using System.Threading;

namespace Jal.RestClient
{
    public interface IRestFluentHandler
    {
        IRestMiddlewareDescriptor Uri(string uri, CancellationToken cancellationtoken = default);

        IRestMiddlewareDescriptor Uri(string uri, System.Net.Http.HttpClient client, CancellationToken cancellationtoken = default);

        IRestMiddlewareDescriptor Uri(string uri, Func<System.Net.Http.HttpClient> factory, CancellationToken cancellationtoken = default);
    }
}