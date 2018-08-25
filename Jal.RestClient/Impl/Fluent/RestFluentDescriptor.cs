using System;
using System.Net;
using System.Threading.Tasks;
using Jal.HttpClient.Impl.Fluent;
using Jal.HttpClient.Interface;
using Jal.HttpClient.Interface.Fluent;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface.Fluent;
using Jal.RestClient.Model;

namespace Jal.RestClient.Impl.Fluent
{

    public class RestFluentDescriptor : IRestMiddlewareDescriptor, IRestQueryParameteDescriptor, IRestMapDescriptor, IRestContentDescriptor
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

            var baseUri = new Uri(_context.Request.Url);

            _context.Request.Url = new Uri(baseUri, path).ToString();

            return this;
        }

        public IRestResourceDescriptor WithTimeout(int timeout)
        {
            if (timeout < 0)
            {
                throw new ArgumentNullException(nameof(timeout));
            }

            _context.Request.Timeout = timeout;

            return this;
        }


        public IRestTimeoutDescriptor WithHeader(Action<IHttpHeaderDescriptor> headerDescriptorAction)
        {

            if (headerDescriptorAction == null)
            {
                throw new ArgumentNullException(nameof(headerDescriptorAction));
            }

            _context.Header = headerDescriptorAction;

            return this;
        }

        public IRestHeaderDescriptor WithMiddleware(Action<IHttpMiddlewareDescriptor> middlewareDescriptorAction)
        {
            if (middlewareDescriptorAction == null)
            {
                throw new ArgumentNullException(nameof(middlewareDescriptorAction));
            }

            _context.Middleware = middlewareDescriptorAction;

            return this;
        }

        public IRestMapDescriptor Get
        {
            get
            {
                _context.Request.HttpMethod = HttpMethod.Get;
                _context.Code = HttpStatusCode.OK;
                return this;
            }
        }

        public IRestMapDescriptor Delete
        {
            get
            {
                _context.Request.HttpMethod = HttpMethod.Delete;
                _context.Code = HttpStatusCode.NoContent;
                return this;
            }
        }

        public IRestContentDescriptor Post
        {
            get
            {
                _context.Request.HttpMethod = HttpMethod.Post;
                _context.Code = HttpStatusCode.Created;
                return this;
            }
        }

        public IRestContentDescriptor Put
        {
            get
            {
                _context.Request.HttpMethod = HttpMethod.Put;
                _context.Code = HttpStatusCode.NoContent;
                return this;
            }
        }

        public IRestContentDescriptor Patch
        {
            get
            {
                _context.Request.HttpMethod = HttpMethod.Patch;
                _context.Code = HttpStatusCode.NoContent;
                return this;
            }
        }

        public IRestVerbDescriptor WithQueryParameter(Action<IHttpQueryParameterDescriptor> queryParemeterDescriptorAction)
        {
            if (queryParemeterDescriptorAction == null)
            {
                throw new ArgumentNullException(nameof(queryParemeterDescriptorAction));
            }

            _context.QueryParameter = queryParemeterDescriptorAction;

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

            _context.Request.AcceptedType = acceptedType;

            return new RestSenderDescriptor<T>(_context, _handler, converter);
        }

        public IRestMapDescriptor Data<TBody>(TBody data, Func<TBody, string> converter, string contentType = "application/json", string characterSet = "charset=utf-8")
        {
            if (converter == null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            if (string.IsNullOrWhiteSpace(characterSet))
            {
                throw new ArgumentNullException(nameof(characterSet));
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentNullException(nameof(contentType));
            }

           var content = converter(data);

            _context.Request.Content = new HttpRequestStringContent(content)
            {
                ContentType = contentType,
                CharacterSet = characterSet
            };

            return this;

        }

        public IRestMapDescriptor Data(string data, string contentType = "application/json", string characterSet = "charset=utf-8")
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (string.IsNullOrWhiteSpace(characterSet))
            {
                throw new ArgumentNullException(nameof(characterSet));
            }

            if (string.IsNullOrWhiteSpace(contentType))
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            _context.Request.Content = new HttpRequestStringContent(data)
            {
                ContentType = contentType,
                CharacterSet = characterSet
            };

            return this;
        }

        public RestResponse Send(HttpIdentity httpIdentity = null)
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



                if (_context.Data != null)
                {
                    var dataDescriptor = new HttpDataDescriptor(_context.Request);

                    _context.Data(dataDescriptor);
                }

                if (httpIdentity != null)
                {
                    _context.Request.Identity = httpIdentity;
                }

                var response = _handler.Send(_context.Request);

                return new RestResponse
                {
                    HttpResponse = response,
                    HttpResquest = _context.Request,
                };
        }

        public async Task<RestResponse> SendAsync(HttpIdentity httpIdentity = null)
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

            if (_context.Data != null)
            {
                var dataDescriptor = new HttpDataDescriptor(_context.Request);

                _context.Data(dataDescriptor);
            }

            if (httpIdentity != null)
            {
                _context.Request.Identity = httpIdentity;
            }

            var response = await _handler.SendAsync(_context.Request);

            return new RestResponse
            {
                HttpResponse = response,
                HttpResquest = _context.Request,
            };
        }

        public IRestHeaderDescriptor WithContext(Action<IHttpDataDescriptor> dataDescriptorAction)
        {
            if (dataDescriptorAction == null)
            {
                throw new ArgumentNullException(nameof(dataDescriptorAction));
            }

            _context.Data = dataDescriptorAction;

            return this;
        }
    }
}
