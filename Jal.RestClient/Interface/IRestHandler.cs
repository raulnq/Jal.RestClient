using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    /// <summary>
    /// https://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.5
    /// </summary>
    public partial interface IRestHandler
    {
        RestResponse Get(string url, IAuthenticator authenticator = null);

        RestResponse Post(string url, string content, string contentType = "application/json", IAuthenticator authenticator = null);

        RestResponse Put(string url, string content, string contentType = "application/json", IAuthenticator authenticator = null);

        RestResponse Delete(string url, IAuthenticator authenticator = null);
    }
}