using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;
using HttpRequest = Jal.HttpClient.Model.HttpRequest;

namespace Jal.RestClient.Impl
{
    public partial class RestHandler
    {
        public async Task<RestResponse> SendAsync(string url, HttpMethod httpMethod, IAuthenticator authenticator = null, string contentType = null, string content = null, string characterSet = null, string acceptedType = null)
        {
            var request = new HttpRequest(url, httpMethod)
                          {
                              ContentType = contentType,
                              Content = content,
                              CharacterSet = characterSet,
                              AcceptedType = acceptedType
                          };

            Authenticate(request, authenticator);

            var response = await HttpHandler.SendAsync(request);

            return new RestResponse
            {
                HttpResponse = response,
                HttpResquest = request
            };
        }

        public Task<RestResponse> GetAsync(string url, string acceptedType = null, IAuthenticator authenticator = null)
        {
            return SendAsync(url, HttpMethod.Get, authenticator, null, null, null, acceptedType);
        }

        public Task<RestResponse> PostAsync(string url, string content, string contentType = "application/json", IAuthenticator authenticator = null)
        {
            return SendAsync(url, HttpMethod.Post, authenticator, contentType, content, "charset=UTF-8");
        }

        public Task<RestResponse> PutAsync(string url, string content, string contentType = "application/json", IAuthenticator authenticator = null)
        {
            return SendAsync(url, HttpMethod.Put, authenticator, contentType, content, "charset=UTF-8");
        }

        public Task<RestResponse> DeleteAsync(string url, IAuthenticator authenticator = null)
        {
            return SendAsync(url, HttpMethod.Delete, authenticator);
        }
    }
}