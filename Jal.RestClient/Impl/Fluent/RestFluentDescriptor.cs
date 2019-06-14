using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Jal.HttpClient.Impl.Fluent;
using Jal.HttpClient.Interface;
using Jal.HttpClient.Interface.Fluent;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface.Fluent;
using Jal.RestClient.Model;

namespace Jal.RestClient.Impl.Fluent
{

    public class RestFluentDescriptor : IRestResourceDescriptor, IRestMiddlewareDescriptor, IRestQueryParameteDescriptor, IRestMapDescriptor, IRestContentDescriptor
    {
        private readonly IHttpHandler _handler;

        private readonly DescriptorContext _context;

        public RestFluentDescriptor(HttpRequest request, IHttpHandler handler)
        {
            _context = new DescriptorContext()
            {
                Request = request
            };
            _handler = handler;
        }

        public IRestQueryParameteDescriptor Path(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var uri = new Uri(_context.Request.Message.RequestUri, path);

            _context.Request.Message.RequestUri = uri;

            return this;
        }

        public IRestResourceDescriptor WithHeader(Action<IHttpHeaderDescriptor> action)
        {
            _context.Header = action ?? throw new ArgumentNullException(nameof(action));

            return this;
        }

        public IRestHeaderDescriptor WithMiddleware(Action<IHttpMiddlewareDescriptor> action)
        {
            _context.Middleware = action ?? throw new ArgumentNullException(nameof(action));

            return this;
        }

        public IRestMapDescriptor Get
        {
            get
            {
                _context.Request.Message.Method = HttpMethod.Get;
                _context.StatusCode = HttpStatusCode.OK;
                return this;
            }
        }

        public IRestMapDescriptor Delete
        {
            get
            {
                _context.Request.Message.Method = HttpMethod.Delete;
                _context.StatusCode = HttpStatusCode.NoContent;
                return this;
            }
        }

        public IRestContentDescriptor Post
        {
            get
            {
                _context.Request.Message.Method = HttpMethod.Post;
                _context.StatusCode = HttpStatusCode.Created;
                return this;
            }
        }

        public IRestContentDescriptor Put
        {
            get
            {
                _context.Request.Message.Method = HttpMethod.Put;
                _context.StatusCode = HttpStatusCode.NoContent;
                return this;
            }
        }

        public IRestContentDescriptor Patch
        {
            get
            {
                _context.Request.Message.Method = new HttpMethod("PATCH");
                _context.StatusCode = HttpStatusCode.NoContent;
                return this;
            }
        }

        public IRestVerbDescriptor WithQueryParameter(Action<IHttpQueryParameterDescriptor> action)
        {
            _context.QueryParameter = action ?? throw new ArgumentNullException(nameof(action));

            return this;
        }

        public IRestWhenDescriptor<T> MapTo<T>(Func<string, T> converter, string acceptedType = "application/json")
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (string.IsNullOrWhiteSpace(acceptedType))
            {
                throw new ArgumentNullException(nameof(acceptedType));
            }

            _context.Request.Message.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(acceptedType));

            return new RestSenderDescriptor<T>(_context, _handler, converter);
        }

        public IRestMapDescriptor Data<TBody>(TBody data, Func<TBody, string> converter, string contentType = "application/json")
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentNullException(nameof(contentType));
            }

           var content = converter(data);

            _context.Request.Content = new StringContent(content);

            _context.Request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

            return this;

        }

        public IRestMapDescriptor Data(string data, string contentType = "application/json")
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            _context.Request.Content = new StringContent(data);

            _context.Request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

            return this;
        }

        public async Task<RestResponse> SendAsync()
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

            return new RestResponse(response);
        }

        public IRestSenderDescriptor WithIdentity(HttpIdentity httpIdentity)
        {
            if (httpIdentity == null)
            {
                throw new ArgumentNullException(nameof(httpIdentity));
            }

            _context.Request.Identity = httpIdentity;

            return this;
        }
    }
}
