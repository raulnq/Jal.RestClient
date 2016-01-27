using System.Linq;
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
            var item = httpRequest.Headers.FirstOrDefault(x => x.Name == "Authorization");
            if (item != null)
            {
                httpRequest.Headers.Remove(item);
            }
            httpRequest.Headers.Add(new HttpHeader() { Value = string.Format("Bearer {0}", _token), Name = "Authorization" });
        }
    }
}
