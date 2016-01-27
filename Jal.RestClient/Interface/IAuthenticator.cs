using Jal.HttpClient.Interface;
using Jal.HttpClient.Model;

namespace Jal.RestClient.Interface
{
    public interface IAuthenticator
    {
        void Authenticate(HttpRequest httpRequest, IHttpHandler httpHandler);
    }
}