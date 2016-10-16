using System;
using Jal.HttpClient.Interface;
using Jal.RestClient.Interface.Fluent;

namespace Jal.RestClient.Impl.Fluent
{
    public class RestFluentHandlerBuilder : IRestFluentHandlerStartBuilder, IRestFluentHandlerEndBuilder
    {
        private IHttpHandler _httpHandler;
        public IRestFluentHandlerEndBuilder UseHttpHandler(IHttpHandler httpHandler)
        {
            if (httpHandler==null)
            {
                throw new ArgumentNullException(nameof(httpHandler));
            }

            _httpHandler = httpHandler;

            return this;
        }

        public IRestFluentHandler Create => new RestFluentHandler(_httpHandler);
    }
}