using System.Linq;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Impl
{
    public class TokenAuthenticator : IAuthenticator
    {
        private readonly string _token;

        private readonly string _type;

        public TokenAuthenticator(string token, string type)
        {
            _token = token;
            _type = type;
        }

        public void Authenticate(HttpRequest httpRequest)
        {
            var item = httpRequest.Headers.FirstOrDefault(x => x.Name == "Authorization");
            if (item != null)
            {
                httpRequest.Headers.Remove(item);
            }
            httpRequest.Headers.Add(new HttpHeader() { Value = $"{_type} {_token}", Name = "Authorization" });
        }
    }
}
