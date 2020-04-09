using System;
using System.Net;
using Jal.HttpClient;

namespace Jal.RestClient
{
    public class DescriptorContext
    {
        public HttpRequest Request { get; set; }

        public Action<IHttpQueryParameterDescriptor> QueryParameter { get; set; }

        public Action<IHttpHeaderDescriptor> Header { get; set; }

        public Action<IHttpMiddlewareDescriptor> Middleware { get; set; }

        public HttpStatusCode? StatusCode { get; set; }

    }
}