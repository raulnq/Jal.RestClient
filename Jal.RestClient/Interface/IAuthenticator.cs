using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    public interface IAuthenticator
    {
        void Authenticate(RestRequest restRequest);
    }
}