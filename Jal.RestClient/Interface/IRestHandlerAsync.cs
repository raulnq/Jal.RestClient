using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    public partial interface IRestHandler
    {
        Task<RestResponse> GetAsync(string url, IAuthenticator authenticator = null);

        Task<RestResponse> PostAsync(string url, string content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null);

        Task<RestResponse> PutAsync(string url, string content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null);

        Task<RestResponse> PostAsync<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null);

        Task<RestResponse> PutAsync<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null);

        Task<RestResponse> DeleteAsync(string url, IAuthenticator authenticator = null);
    }
}
