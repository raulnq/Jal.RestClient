using Jal.HttpClient.Model;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    public interface IRestHandler
    {
        RestResponse<TResponse> Execute<TRequest, TResponse>(HttpRequest httpRequest, TRequest request=null, RestAuthenticationInfo restAuthenticationInfo = null) where TResponse : class, new() where TRequest : class, new();

        RestResponse<TResponse> Execute<TResponse>(RestRequest request) where TResponse : class, new();

        RestResponse Execute<TRequest>(HttpRequest httpRequest, TRequest request, RestAuthenticationInfo restAuthenticationInfo = null) where TRequest : class, new();

        RestResponse Execute(RestRequest request);
    }
}