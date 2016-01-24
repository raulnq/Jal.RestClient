using Jal.HttpClient.Interface;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;

namespace Jal.RestClient.Impl
{
    public class BearerTokenAuthenticator : IAuthenticator
    {
        private readonly string _token;

        public BearerTokenAuthenticator(string token)
        {
            _token = token;
        }

        public void Authenticate(HttpRequest httpRequest, IHttpHandler httpHandler)
        {
            httpRequest.AddHeader("Authorization", string.Format("Bearer {0}", _token));
        }
    }
}
