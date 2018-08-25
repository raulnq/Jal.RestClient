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
        public void Authenticate(HttpRequest httpRequest)
        {

        }

        public HttpResponse Send(HttpRequest request, Func<HttpResponse> next)
        {
            var item = request.Headers.FirstOrDefault(x => x.Name == "Authorization");

            if (item != null)
            {
                request.Headers.Remove(item);
            }

            var token = request.Data["token"];

            var type = request.Data["tokentype"];

            request.Headers.Add(new HttpHeader("Authorization", $"{type} {token}"));

            return next();
        }

        public async Task<HttpResponse> SendAsync(HttpRequest request, Func<Task<HttpResponse>> next)
        {
            var item = request.Headers.FirstOrDefault(x => x.Name == "Authorization");

            if (item != null)
            {
                request.Headers.Remove(item);
            }

            var token = request.Data["token"];

            var type = request.Data["tokentype"];

            request.Headers.Add(new HttpHeader("Authorization", $"{type} {token}"));

            return await next();
        }
    }
}
