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

        public IAuthenticator Authenticator { get; set; }

        public RestHandler(IHttpHandler httpHandler, IModelConverter modelConverter)
        {
            _httpHandler = httpHandler;

            _modelConverter = modelConverter;

            Authenticator = NullAuthenticator.Instance;
        }

        public RestResponse Send(string url, HttpMethod httpMethod, RestAuthenticationInfo restAuthenticationInfo = null, HttpContentType httpContentType = HttpContentType.Form, string body = null)
        {
            var request = new HttpRequest(url, httpMethod, httpContentType);

            Authenticate(request, restAuthenticationInfo);

            request.Body = body;

            var response = _httpHandler.Send(request);

            return new RestResponse
            {
                HttpResponse = response
            };
        }

        public RestResponse<TResponse> Get<TResponse>(string url, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            var request = new HttpRequest(url, HttpMethod.Get, HttpContentType.Form);

            Authenticate(request, restAuthenticationInfo);

            var response = _httpHandler.Send(request);

            if (response != null && response.HttpStatusCode == HttpStatusCode.OK)
            {
                return new RestResponse<TResponse>
                {
                    HttpResponse = response,
                    Result = _modelConverter.Convert<string, TResponse>(response.Content)
                };
            }
            else
            {
                return new RestResponse<TResponse>
                {
                    HttpResponse = response
                };
            }
        }

        public RestResponse Get(string url, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            return Send(url, HttpMethod.Get, restAuthenticationInfo);
        }

        public RestResponse Post<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            var body = _modelConverter.Convert<TContent, string>(content);

            return Send(url, HttpMethod.Post, restAuthenticationInfo, httpContentType, body);
        }

        public RestResponse Put<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            var body = _modelConverter.Convert<TContent, string>(content);

            return Send(url, HttpMethod.Put, restAuthenticationInfo, httpContentType, body);
        }

        public RestResponse Delete(string url, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            return Send(url, HttpMethod.Delete, restAuthenticationInfo);
        }

        private void Authenticate(HttpRequest request, RestAuthenticationInfo restAuthenticationInfo)
        {
            if (restAuthenticationInfo != null)
            {
                Authenticator.Authenticate(request, restAuthenticationInfo);
            }
        }
    }
}