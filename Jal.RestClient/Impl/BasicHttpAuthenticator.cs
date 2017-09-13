using System;
using System.Linq;
using System.Text;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Impl
{
    public class BasicHttpAuthenticator : IAuthenticator
    {
        private readonly string _user;

        private readonly string _password;

        public BasicHttpAuthenticator(string user, string password)
        {
            _user = user;
            _password = password;
        }

        public void Authenticate(HttpRequest httpRequest)
        {
            var item = httpRequest.Headers.FirstOrDefault(x => x.Name == "Authorization");
            if (item != null)
            {
                httpRequest.Headers.Remove(item);
            }
            httpRequest.Headers.Add(new HttpHeader("Authorization", $"{"Basic"} {Convert.ToBase64String(Encoding.UTF8.GetBytes(_user + ":" + _password))}"));
        }
    }
}