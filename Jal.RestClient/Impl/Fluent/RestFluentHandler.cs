using System;
using System.Net.Http;
using System.Threading;
using Jal.HttpClient;

namespace Jal.RestClient
{
    public class RestFluentHandler : IRestFluentHandler 
    {
        private readonly IHttpHandler _handler;

        public RestFluentHandler(IHttpHandler handler)
        {
            _handler = handler;
        }

        public IRestMiddlewareDescriptor Uri(string uri, CancellationToken cancellationtoken = default)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var request = new HttpRequest(uri, HttpMethod.Get, cancellationtoken);

            return new RestFluentDescriptor(request, _handler);
        }

        public IRestMiddlewareDescriptor Uri(string uri, System.Net.Http.HttpClient client, CancellationToken cancellationtoken = default)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var request = new HttpRequest(uri, HttpMethod.Get, client, cancellationtoken);

            return new RestFluentDescriptor(request, _handler);
        }

        public IRestMiddlewareDescriptor Uri(string uri, Func<System.Net.Http.HttpClient> factory, CancellationToken cancellationtoken = default)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var request = new HttpRequest(uri, HttpMethod.Get, factory, cancellationtoken);

            return new RestFluentDescriptor(request, _handler);
        }

    }
}