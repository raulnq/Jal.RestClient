using System.Linq;
using System.Net;
using Jal.Converter.Interface;
using Jal.HttpClient.Interface;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;
using HttpRequest = Jal.HttpClient.Model.HttpRequest;

namespace Jal.RestClient.Impl
{
    public partial class RestHandler : IRestHandler
    {
        readonly IHttpHandler _httpHandler;

        readonly IModelConverter _modelConverter;

        public RestHandler(IHttpHandler httpHandler, IModelConverter modelConverter)
        {
            _httpHandler = httpHandler;

            _modelConverter = modelConverter;
        }

        public RestResponse<TResponse> To<TResponse>(RestResponse response, HttpStatusCode[] httpStatusCodes)
        {
            if (response != null && response.HttpResponse != null && httpStatusCodes.Contains(response.HttpResponse.HttpStatusCode))
            {
                return new RestResponse<TResponse>
                {
                    HttpResponse = response.HttpResponse,
                    Result = _modelConverter.Convert<string, TResponse>(response.HttpResponse.Content)
                };
            }
            else
            {
                return new RestResponse<TResponse>
                {
                    HttpResponse = response.HttpResponse
                };
            }
        }

        public RestResponse Send(string url, HttpMethod httpMethod, IAuthenticator authenticator = null, HttpContentType httpContentType = HttpContentType.Form, string body = null)
        {
            var request = new HttpRequest(url, httpMethod, httpContentType);

            Authenticate(request, authenticator);

            request.Body = body;

            var response = _httpHandler.Send(request);

            return new RestResponse
            {
                HttpResponse = response
            };
        }

        public RestResponse Get(string url, IAuthenticator authenticator = null)
        {
            return Send(url, HttpMethod.Get, authenticator);
        }

        public RestResponse Post<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null)
        {
            var body = _modelConverter.Convert<TContent, string>(content);

            return Send(url, HttpMethod.Post, authenticator, httpContentType, body);
        }

        public RestResponse Put<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null)
        {
            var body = _modelConverter.Convert<TContent, string>(content);

            return Send(url, HttpMethod.Put, authenticator, httpContentType, body);
        }

        public RestResponse Post(string url, string content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null)
        {
            return Send(url, HttpMethod.Post, authenticator, httpContentType, content);
        }

        public RestResponse Put(string url, string content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null)
        {
            return Send(url, HttpMethod.Put, authenticator, httpContentType, content);
        }

        public RestResponse Delete(string url, IAuthenticator authenticator = null)
        {
            return Send(url, HttpMethod.Delete, authenticator);
        }

        private void Authenticate(HttpRequest request, IAuthenticator authenticator)
        {
            if (authenticator != null)
            {
                authenticator.Authenticate(request, _httpHandler);
            }
        }
    }
}