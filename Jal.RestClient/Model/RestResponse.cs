using Jal.HttpClient.Model;

namespace Jal.RestClient.Model
{
    public class RestResponse
    {
        public HttpResponse HttpResponse { get; set; }
    }

    public class RestResponse<T> : RestResponse
    {
        public T Result { get; set; }
    }
}