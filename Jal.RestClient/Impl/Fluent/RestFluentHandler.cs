using System;
using System.Net.Http;
using Jal.HttpClient.Interface;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface.Fluent;

namespace Jal.RestClient.Impl.Fluent
{
    public class RestFluentHandler : IRestFluentHandler 
    {
        private readonly IHttpHandler _handler;

        public RestFluentHandler(IHttpHandler handler)
        {
            _handler = handler;
        }

        public IRestMiddlewareDescriptor Uri(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var request = new HttpRequest(uri, HttpMethod.Get);

            return new RestFluentDescriptor(request, _handler);
        }

        public IRestMiddlewareDescriptor Uri(string uri, System.Net.Http.HttpClient client)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var request = new HttpRequest(uri, HttpMethod.Get, client);

            return new RestFluentDescriptor(request, _handler);
        }

        public IRestMiddlewareDescriptor Uri(string uri, Func<System.Net.Http.HttpClient> factory)
        {
            if (string.IsNullOrWhiteSpace(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var request = new HttpRequest(uri, HttpMethod.Get, factory);

            return new RestFluentDescriptor(request, _handler);
        }

    }
}