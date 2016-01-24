using System;
using System.Text;
using Jal.HttpClient.Interface;
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

        public void Authenticate(HttpRequest httpRequest, IHttpHandler httpHandler)
        {
            httpRequest.AddHeader("Authorization", string.Format("{0} {1}", "Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(_user + ":" + _password))));
        }
    }
}