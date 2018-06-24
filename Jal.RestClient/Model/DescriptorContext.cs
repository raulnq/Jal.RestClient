﻿using System;
using System.Net;
using Jal.HttpClient.Interface.Fluent;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Model
{
    public class DescriptorContext
    {
        public HttpRequest Request { get; set; }

        public Action<IHttpQueryParameterDescriptor> QueryParameter { get; set; }

        public Action<IHttpHeaderDescriptor> Header { get; set; }

        public Action<HttpRequest> Authenticator { get; set; }

        public HttpStatusCode? Code { get; set; }

    }
}