using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;

namespace Jal.RestClient.Fluent
{
    public class RestHandlerDescriptor : IContentDescriptor, IAuthenticatorDescriptor
    {
        protected IAuthenticator Authenticator;

        protected readonly Func<string, IAuthenticator, string, string, RestResponse> Verb;

        protected readonly Func<string, IAuthenticator, string, string, Task<RestResponse>> VerbAsync;

        protected readonly string Url;

        protected string ContentType;

        protected string Content;

        public RestHandlerDescriptor(string url, Func<string, IAuthenticator, string, string, RestResponse> verb, Func<string, IAuthenticator, string, string, Task<RestResponse>> verbAsync)
        {
            Verb = verb;
            VerbAsync = verbAsync;
            Url = url;
        }

        public RestResponse Send()
        {
            return Verb(Url, Authenticator, Content, ContentType);
        }

        public Task<RestResponse> SendAsync()
        {
            return VerbAsync(Url, Authenticator, Content, ContentType);
        }

        public ISenderDescriptor WithAuthenticator(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
            return this;
        }

        public IContentDescriptor WithContent(string content)
        {
            Content = content;
            return this;
        }

        public IContentDescriptor WithContent<T>(T content, Func<T, string> converter)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }
            Content = converter(content);
            return this;
        }

        public RestResponse<T> Send<T>(Func<string, T> converter, HttpStatusCode[] successfulStatusCodes)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }

            var response = Verb(Url, Authenticator, Content, ContentType);

            if (response != null && response.HttpResponse != null && !string.IsNullOrEmpty(response.HttpResponse.Content) && successfulStatusCodes.Contains(response.HttpResponse.HttpStatusCode))
            { 
                return new RestResponse<T>()
                       {
                           HttpResponse = response.HttpResponse,
                           HttpResquest = response.HttpResquest,
                           Result = converter(response.HttpResponse.Content)
                       };
            }
            return new RestResponse<T>()
                    {
                        HttpResponse = response.HttpResponse,
                        HttpResquest = response.HttpResquest,
                    };
        }

        public async Task<RestResponse> SendAsync<T>(Func<string, T> converter, HttpStatusCode[] successfulStatusCodes)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter");
            }

            var response =await VerbAsync(Url, Authenticator, Content, ContentType);

            if (response != null && response.HttpResponse != null && !string.IsNullOrEmpty(response.HttpResponse.Content) && successfulStatusCodes.Contains(response.HttpResponse.HttpStatusCode))
            {
                return new RestResponse<T>()
                {
                    HttpResponse = response.HttpResponse,
                    HttpResquest = response.HttpResquest,
                    Result = converter(response.HttpResponse.Content)
                };
            }
            return new RestResponse<T>()
            {
                HttpResponse = response.HttpResponse,
                HttpResquest = response.HttpResquest,
            };
        }

        public IContentDescriptor WithContentType(string contentType)
        {
            this.ContentType = contentType;
            return this;
        }
    }
}