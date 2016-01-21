using System.Net;
using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Model;
using HttpRequest = Jal.HttpClient.Model.HttpRequest;

namespace Jal.RestClient.Impl
{
    public partial class RestHandler
    {
        public async Task<RestResponse> SendAsync(string url, HttpMethod httpMethod, RestAuthenticationInfo restAuthenticationInfo = null, HttpContentType httpContentType = HttpContentType.Form, string body = null)
        {
            var request = new HttpRequest(url, httpMethod, httpContentType);

            Authenticate(request, restAuthenticationInfo);

            request.Body = body;

            var response = await _httpHandler.SendAsync(request);

            return new RestResponse
            {
                HttpResponse = response
            };
        }

        public async Task<RestResponse<TResponse>> GetAsync<TResponse>(string url, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            var request = new HttpRequest(url, HttpMethod.Get, HttpContentType.Form);

            Authenticate(request, restAuthenticationInfo);

            var response = await _httpHandler.SendAsync(request);

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

        public Task<RestResponse> PostAsync<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            var body = _modelConverter.Convert<TContent, string>(content);

            return SendAsync(url, HttpMethod.Post, restAuthenticationInfo, httpContentType, body);
        }

        public Task<RestResponse> PutAsync<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            var body = _modelConverter.Convert<TContent, string>(content);

            return SendAsync(url, HttpMethod.Put, restAuthenticationInfo, httpContentType, body);
        }

        public Task<RestResponse> DeleteAsync(string url, RestAuthenticationInfo restAuthenticationInfo = null)
        {
            return SendAsync(url, HttpMethod.Delete, restAuthenticationInfo);
        }
    }
}