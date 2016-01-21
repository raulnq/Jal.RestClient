using Jal.HttpClient.Model;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;

namespace Jal.RestClient.Impl
{
    public class NullAuthenticator : IAuthenticator
    {
        public static NullAuthenticator Instance = new NullAuthenticator();

        public void Authenticate(HttpRequest httpRequest, RestAuthenticationInfo restAuthenticationInfo)
        {
            
        }
    }
}
