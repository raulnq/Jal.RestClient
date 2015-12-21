using System.Net;
using Jal.Converter.Interface;
using Jal.HttpClient.Interface;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;
using HttpRequest = Jal.HttpClient.Model.HttpRequest;

namespace Jal.RestClient.Impl
{
    public class RestHandler : IRestHandler
    {
        readonly IHttpHandler _httpHandler;

        readonly IModelConverter _modelConverter;

        public IAuthenticator Authenticator { get; set; }

        public RestHandler(IHttpHandler httpHandler, IModelConverter modelConverter)
        {
            _httpHandler = httpHandler;

            _modelConverter = modelConverter;

            Authenticator = NullAuthenticator.Instance;
        }

        public RestResponse<TResponse> Execute<TRequest, TResponse>(HttpRequest httpRequest, TRequest request=null, RestAuthenticationInfo restAuthenticationInfo = null) where TResponse : class, new() where TRequest : class, new()
        {
            var content = _modelConverter.Convert<TRequest, string>(request);

            if (httpRequest != null)
            {
                httpRequest.Body = content;

                var restRequest = new RestRequest(httpRequest, restAuthenticationInfo);

                return Execute<TResponse>(restRequest);
            }
            else
            {
                return new RestResponse<TResponse>
                {
                    ErrorMessage = string.Format("Problems at the moment to create the request to {0}", httpRequest.Url),
                    Succeeded = false
                };
            }
        }

        public RestResponse<TResponse> Execute<TResponse>(RestRequest request) where TResponse : class, new()
        {
            Authenticator.Authenticate(request);

            var response = _httpHandler.Send(request.HttpRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK && response.WebExceptionStatus == WebExceptionStatus.Success)
            {
                if (!string.IsNullOrWhiteSpace(response.Content))
                {
                    var restResponse = new RestResponse<TResponse>();

                    var result = _modelConverter.Convert<string, TResponse>(response.Content);

                    if (result != null)
                    {
                        restResponse.HttpResponse = response;

                        restResponse.Result = result;

                        return restResponse;
                    }
                    else
                    {
                        return new RestResponse<TResponse>
                        {
                            HttpResponse = response,
                            ErrorMessage = string.Format("Problems at the moment to create the response to {0}", request.HttpRequest.Url),
                            Succeeded = false
                        };
                    }
                }
                else
                {
                    return new RestResponse<TResponse>
                    {
                        HttpResponse = response,
                        ErrorMessage = string.Format("The response to {0} is empty", request.HttpRequest.Url),
                        Succeeded = false
                    };
                }
            }
            else
            {
                return new RestResponse<TResponse>
                {
                    HttpResponse = response,
                    ErrorMessage = string.Format("{0}-{1}", response.ErrorMessage, response.Content),
                    Succeeded = false
                };
            }
        }

        public RestResponse Execute<TRequest>(HttpRequest httpRequest, TRequest request, RestAuthenticationInfo restAuthenticationInfo = null) where TRequest : class, new()
        {
            var content = _modelConverter.Convert<TRequest, string>(request);

            if (httpRequest != null)
            {
                httpRequest.Body = content;

                var restRequest = new RestRequest(httpRequest, restAuthenticationInfo);

                return Execute(restRequest);
            }
            else
            {
                return new RestResponse
                {
                    ErrorMessage = string.Format("Problems at the moment to create the request to {0}", httpRequest.Url),
                    Succeeded = false
                };
            }
        }

        public RestResponse Execute(RestRequest request)
        {

            Authenticator.Authenticate(request);

            var response = _httpHandler.Send(request.HttpRequest);

            if (response.HttpStatusCode == HttpStatusCode.OK && response.WebExceptionStatus == WebExceptionStatus.Success)
            {
                return new RestResponse
                {
                    HttpResponse = response,
                    Succeeded = true
                };
            }
            else
            {
                return new RestResponse
                {
                    ErrorMessage = string.Format("{0}-{1}", response.ErrorMessage, response.Content),
                    Succeeded = false,
                    HttpResponse = response,
                };
            }
        }
    }
}