using System;
using System.Net;
using System.Threading.Tasks;
using Jal.HttpClient.Impl.Fluent;
using Jal.HttpClient.Interface;
using Jal.RestClient.Interface.Fluent;
using Jal.RestClient.Model;

namespace Jal.RestClient.Impl.Fluent
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

        public RestResponse<T> Send
        {
            get
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

                _context.Authenticator?.Invoke(_context.Request);

                var response = _handler.Send(_context.Request);

                var content = response?.Content?.Read();

                var result = !string.IsNullOrEmpty(content) && _context.Code == response.HttpStatusCode  ? _converter(content) : default(T);

                return new RestResponse<T>
                       {
                           HttpResponse = response,
                           HttpResquest = _context.Request,
                           Data = result
                       };
            }
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

            _context.Authenticator?.Invoke(_context.Request);

            var response = await _handler.SendAsync(_context.Request);

            var content = response?.Content?.Read();

            var result = !string.IsNullOrEmpty(content) && _context.Code == response.HttpStatusCode ? _converter(content) : default(T);

            return new RestResponse<T>
                   {
                       HttpResponse = response,
                       HttpResquest = _context.Request,
                       Data = result
                   };
        }

        public IRestSenderDescriptor<T> When(HttpStatusCode httpStatusCode)
        {
            _context.Code = httpStatusCode;

            return this;
        }
    }
}