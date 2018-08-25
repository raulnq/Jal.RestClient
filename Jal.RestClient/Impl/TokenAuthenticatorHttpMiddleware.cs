using System;
using System.Linq;
using System.Threading.Tasks;
using Jal.HttpClient.Interface;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Impl
{
    public class TokenAuthenticatorHttpMiddleware : IHttpMiddleware
    {
        private readonly string _token;

        private readonly string _type;

        public TokenAuthenticatorHttpMiddleware(string token, string type)
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
            httpRequest.Headers.Add(new HttpHeader("Authorization", $"{_type} {_token}"));
        }

        public HttpResponse Send(HttpRequest request, Func<HttpResponse> next)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponse> SendAsync(HttpRequest request, Func<Task<HttpResponse>> next)
        {
            throw new NotImplementedException();
        }
    }
}
