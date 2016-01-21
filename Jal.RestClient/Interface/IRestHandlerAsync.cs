using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    public partial interface IRestHandler
    {
        Task<RestResponse<TResponse>> GetAsync<TResponse>(string url, RestAuthenticationInfo restAuthenticationInfo = null);

        Task<RestResponse> PostAsync<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, RestAuthenticationInfo restAuthenticationInfo = null);

        Task<RestResponse> PutAsync<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, RestAuthenticationInfo restAuthenticationInfo = null);

        Task<RestResponse> DeleteAsync(string url, RestAuthenticationInfo restAuthenticationInfo = null);
    }
}
