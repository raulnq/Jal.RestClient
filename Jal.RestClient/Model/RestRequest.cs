using Jal.HttpClient.Model;

namespace Jal.RestClient.Model
{
    public class RestRequest
    {
        public HttpRequest HttpRequest { get; set; }

        public RestAuthenticationInfo RestAuthenticationInfo { get; set; }

        public RestRequest(HttpRequest httpRequest, RestAuthenticationInfo restAuthenticationInfo)
        {
            HttpRequest = httpRequest;

            RestAuthenticationInfo = restAuthenticationInfo;
        }

        public RestRequest(HttpRequest httpRequest)
        {
            HttpRequest = httpRequest;
        }
    }
}