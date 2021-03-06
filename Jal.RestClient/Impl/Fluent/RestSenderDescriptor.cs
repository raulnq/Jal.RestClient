﻿using System;
using System.Net;
using System.Threading.Tasks;
using Jal.HttpClient;

namespace Jal.RestClient
{
    public class RestSenderDescriptor<T> : IRestWhenDescriptor<T>
    {
        private readonly DescriptorContext _context;

        private readonly IHttpHandler _handler;

        private readonly Func<string, T> _converter;

        public RestSenderDescriptor(DescriptorContext context, IHttpHandler handler, Func<string, T> converter)
        {
            _context = context;
            _handler = handler;
            _converter = converter;
        }

        public IRestSenderDescriptor<T> Always()
        {
            _context.StatusCode = null;

            return this;
        }

        public async Task<RestResponse<T>> SendAsync()
        {
            if (_context.QueryParameter != null)
            {
                var queryParemeterDescriptor = new HttpQueryParameterDescriptor(_context.Request);
                _context.QueryParameter(queryParemeterDescriptor);
            }

            if (_context.Header != null)
            {
                var headerDescriptor = new HttpHeaderDescriptor(_context.Request);
                _context.Header(headerDescriptor);
            }

            if (_context.Middleware != null)
            {
                var middlewareDescriptor = new HttpMiddlewareDescriptor(_context.Request);

                _context.Middleware(middlewareDescriptor);
            }

            var response = await _handler.SendAsync(_context.Request);

            if(response.Message!=null)
            {
                var content = await response.Message.Content.ReadAsStringAsync();

                var result = !string.IsNullOrEmpty(content) && (_context.StatusCode == null || _context.StatusCode == response.Message.StatusCode) ? _converter(content) : default(T);

                return new RestResponse<T>(response, result);

            }

            return new RestResponse<T>(response, default(T));
        }

        public IRestSenderDescriptor<T> When(HttpStatusCode statuscode)
        {
            _context.StatusCode = statuscode;

            return this;
        }

        public IRestSenderDescriptor<T> WithTracing(HttpTracingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            _context.Request.Tracing = context;

            return this;
        }
    }
}