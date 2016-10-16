using System;
using Jal.HttpClient.Impl.Fluent;
using Jal.HttpClient.Interface;
using Jal.HttpClient.Interface.Fluent;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface.Fluent;

namespace Jal.RestClient.Impl.Fluent
{
    public class RestFluentHandler : IRestFluentHandler 
    {
        public static IRestFluentHandler Current;

        public static IRestFluentHandlerStartBuilder Builder => new RestFluentHandlerBuilder();

        private readonly IHttpHandler _handler;

        public RestFluentHandler(IHttpHandler handler)
        {
            _handler = handler;
        }

        public IRestAuthenticatorDescriptor Url(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            var request = new HttpRequest(url, HttpMethod.Get);

            return new RestFluentDescriptor(request, _handler);
        }

    }
}