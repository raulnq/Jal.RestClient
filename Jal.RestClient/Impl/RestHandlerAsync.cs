using System.Net;
using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;
using HttpRequest = Jal.HttpClient.Model.HttpRequest;

namespace Jal.RestClient.Impl
{
    public partial class RestHandler
    {
        public async Task<RestResponse> SendAsync(string url, HttpMethod httpMethod, IAuthenticator authenticator = null, HttpContentType httpContentType = HttpContentType.Form, string body = null)
        {
            var request = new HttpRequest(url, httpMethod, httpContentType);

            Authenticate(request, authenticator);

            request.Body = body;

            var response = await _httpHandler.SendAsync(request);

            return new RestResponse
            {
                HttpResponse = response
            };
        }

        public Task<RestResponse> GetAsync(string url, IAuthenticator authenticator = null)
        {
            return SendAsync(url, HttpMethod.Get, authenticator);
        }

        public Task<RestResponse> PostAsync<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null)
        {
            var body = _modelConverter.Convert<TContent, string>(content);

            return SendAsync(url, HttpMethod.Post, authenticator, httpContentType, body);
        }

        public Task<RestResponse> PutAsync<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null)
        {
            var body = _modelConverter.Convert<TContent, string>(content);

            return SendAsync(url, HttpMethod.Put, authenticator, httpContentType, body);
        }

        public Task<RestResponse> PostAsync(string url, string content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null)
        {
            return SendAsync(url, HttpMethod.Post, authenticator, httpContentType, content);
        }

        public Task<RestResponse> PutAsync(string url, string content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null)
        {
            return SendAsync(url, HttpMethod.Put, authenticator, httpContentType, content);
        }

        public Task<RestResponse> DeleteAsync(string url, IAuthenticator authenticator = null)
        {
            return SendAsync(url, HttpMethod.Delete, authenticator);
        }
    }
}