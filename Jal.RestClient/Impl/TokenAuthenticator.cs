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

        public void Authenticate(RestRequest restRequest)
        {
            restRequest.HttpRequest.AddHeader("Authorization", string.Format("{0} {1}", _tokenName, restRequest.RestAuthenticationInfo.Token));
        }
    }
}
