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

    public class RestFluentDescriptor : IRestAuthenticatorDescriptor, IRestQueryParameteDescriptor, IRestMapDescriptor, IRestContentDescriptor
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


        public IRestTimeoutDescriptor WithHeader(Action<IHeaderDescriptor> headerDescriptorAction)
        {

            if (headerDescriptorAction == null)
            {
                throw new ArgumentNullException(nameof(headerDescriptorAction));
            }

            _context.Header = headerDescriptorAction;

            return this;
        }

        public IRestHeaderDescriptor AuthorizedBy(Action<HttpRequest> authenticator)
        {
            if (authenticator == null)
            {
                throw new ArgumentNullException(nameof(authenticator));
            }

            _context.Authenticator = authenticator;

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
                _context.Code = HttpStatusCode.OK;
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
                _context.Code = HttpStatusCode.OK;
                return this;
            }
        }

        public IRestContentDescriptor Patch
        {
            get
            {
                _context.Request.HttpMethod = HttpMethod.Patch;
                _context.Code = HttpStatusCode.OK;
                return this;
            }
        }

        public IRestVerbDescriptor WithQueryParameter(Action<IQueryParameterDescriptor> queryParemeterDescriptorAction)
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

        public IRestMapDescriptor Data<TBody>(TBody data, Func<TBody, string> converter, string contentType = "application/json", string characterSet = "charset=UTF-8")
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

            _context.Request.ContentType = contentType;
            _context.Request.CharacterSet = characterSet;
            _context.Request.Content = converter(data);
            return this;

        }

        public IRestMapDescriptor Data(string data, string contentType = "application/json", string characterSet = "charset=UTF-8")
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

            _context.Request.ContentType = contentType;
            _context.Request.CharacterSet = characterSet;
            _context.Request.Content = data;
            return this;
        }

        public RestResponse Send
        {
            get
            {
                if (_context.QueryParameter != null)
                {
                    var queryParemeterDescriptor = new QueryParameterDescriptor(_context.Request);
                    _context.QueryParameter(queryParemeterDescriptor);
                }

                if (_context.Header != null)
                {
                    var headerDescriptor = new HeaderDescriptor(_context.Request);
                    _context.Header(headerDescriptor);
                }

                _context.Authenticator?.Invoke(_context.Request);

                var response = _handler.Send(_context.Request);

                return new RestResponse
                {
                    HttpResponse = response,
                    HttpResquest = _context.Request,
                };
            }
        }

        public async Task<RestResponse> SendAsync()
        {
            if (_context.QueryParameter != null)
            {
                var queryParemeterDescriptor = new QueryParameterDescriptor(_context.Request);
                _context.QueryParameter(queryParemeterDescriptor);
            }

            if (_context.Header != null)
            {
                var headerDescriptor = new HeaderDescriptor(_context.Request);
                _context.Header(headerDescriptor);
            }

            _context.Authenticator?.Invoke(_context.Request);

            var response = await _handler.SendAsync(_context.Request);

            return new RestResponse
            {
                HttpResponse = response,
                HttpResquest = _context.Request,
            };
        }
    }
}
