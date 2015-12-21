using Jal.RestClient.Interface;
using Jal.RestClient.Model;

namespace Jal.RestClient.Impl
{
    public class BearerAuthenticator : IAuthenticator
    {
        public void Authenticate(RestRequest restRequest)
        {
            restRequest.HttpRequest.AddHeader("Authorization", string.Format("{0} {1}", restRequest.RestAuthenticationInfo.TokenName, restRequest.RestAuthenticationInfo.Token));
        }
    }
}
