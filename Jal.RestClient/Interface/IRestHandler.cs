using System.Net;
using Jal.HttpClient.Model;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    /// <summary>
    /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.5
    /// </summary>
    public partial interface IRestHandler
    {
        RestResponse Get(string url, IAuthenticator authenticator = null);

        RestResponse Post(string url, string content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null);

        RestResponse Put(string url, string content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null);

        RestResponse Post<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null);

        RestResponse Put<TContent>(string url, TContent content, HttpContentType httpContentType = HttpContentType.Json, IAuthenticator authenticator = null);

        RestResponse Delete(string url, IAuthenticator authenticator = null);

        RestResponse<TResponse> To<TResponse>(RestResponse response, HttpStatusCode[] httpStatusCodes);
    }
}