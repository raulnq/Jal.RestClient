using Jal.HttpClient.Model;

namespace Jal.RestClient.Model
{
    public class RestResponse
    {
        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }

        public HttpResponse HttpResponse { get; set; }

        public RestResponse()
        {
            Succeeded = true;
        }

        public RestResponse(string errorMessage)
        {
            Succeeded = false;
            ErrorMessage = errorMessage;
        }
    }

    public class RestResponse<T> : RestResponse where T : class, new()
    {
        public T Result { get; set; }

        public RestResponse()
            : base()
        {
            Result = new T();
        }

        public RestResponse(string errorMessage)
            : base(errorMessage)
        {
            Result = null;
        }
    }
}