using Jal.HttpClient.Model;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;

namespace Jal.RestClient.Impl
{
    public class TokenAuthenticator : IAuthenticator
    {
        private readonly string _tokenName;

        public TokenAuthenticator(string tokenName)
        {
            _tokenName = tokenName;
        }

        public void Authenticate(HttpRequest httpRequest, RestAuthenticationInfo restAuthenticationInfo)
        {
            httpRequest.AddHeader("Authorization", string.Format("{0} {1}", _tokenName, restAuthenticationInfo.Token));
        }
    }
}
