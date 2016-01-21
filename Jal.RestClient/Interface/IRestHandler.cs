using Jal.HttpClient.Model;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    /// <summary>
    /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.5
    /// </summary>
    public partial interface IRestHandler
    {
        RestResponse<TResponse> Get<TResponse>(string url, RestAuthenticationInfo restAuthenticationInfo = null);

        RestResponse Post<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, RestAuthenticationInfo restAuthenticationInfo = null);

        RestResponse Put<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, RestAuthenticationInfo restAuthenticationInfo = null);

        RestResponse Delete(string url, RestAuthenticationInfo restAuthenticationInfo = null);
    }
}