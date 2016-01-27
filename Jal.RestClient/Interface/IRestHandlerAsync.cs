using System.Threading.Tasks;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    public partial interface IRestHandler
    {
        Task<RestResponse> GetAsync(string url, IAuthenticator authenticator = null);

        Task<RestResponse> PostAsync(string url, string content, string contentType = "application/json", IAuthenticator authenticator = null);

        Task<RestResponse> PutAsync(string url, string content, string contentType = "application/json", IAuthenticator authenticator = null);

        Task<RestResponse> DeleteAsync(string url, IAuthenticator authenticator = null);
    }
}
