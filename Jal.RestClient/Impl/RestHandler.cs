﻿using Jal.HttpClient.Interface;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;
using HttpRequest = Jal.HttpClient.Model.HttpRequest;

namespace Jal.RestClient.Impl
{
    public partial class RestHandler : IRestHandler
    {
        public IHttpHandler HttpHandler { get; private set; }

        public RestHandler(IHttpHandler httpHandler)
        {
            HttpHandler = httpHandler;
        }

        public RestResponse Send(string url, HttpMethod httpMethod, IAuthenticator authenticator = null, string contentType = null, string content = null, string httpCharacterSet = null, string acceptedType = null)
        {
            var request = new HttpRequest(url, httpMethod)
                          {
                              ContentType = contentType,
                              Content = content,
                              CharacterSet = httpCharacterSet,
                              AcceptedType = acceptedType
                          };

            Authenticate(request, authenticator);

            var response = HttpHandler.Send(request);

            return new RestResponse
            {
                HttpResponse = response,
                HttpResquest = request
            };
        }

        public RestResponse Get(string url, string acceptedType = null, IAuthenticator authenticator = null)
        {
            return Send(url, HttpMethod.Get, authenticator, null, null, null, acceptedType);
        }

        public RestResponse Post(string url, string content, string contentType = "application/json", IAuthenticator authenticator = null)
        {
            return Send(url, HttpMethod.Post, authenticator, contentType, content, "charset=UTF-8");
        }

        public RestResponse Put(string url, string content, string contentType = "application/json", IAuthenticator authenticator = null)
        {
            return Send(url, HttpMethod.Put, authenticator, contentType, content, "charset=UTF-8");
        }

        public RestResponse Delete(string url, IAuthenticator authenticator = null)
        {
            return Send(url, HttpMethod.Delete, authenticator);
        }

        private void Authenticate(HttpRequest request, IAuthenticator authenticator)
        {
            if (authenticator != null)
            {
                authenticator.Authenticate(request, this);
            }
        }
    }
}