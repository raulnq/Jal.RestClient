using System;
using Jal.HttpClient.Model;

namespace Jal.RestClient.Model
{
    public class RestResponse : IDisposable
    {
        public HttpResponse HttpResponse { get; set; }

        public HttpRequest HttpResquest { get; set; }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                HttpResponse?.Dispose();
                HttpResquest = null;
            }
        }
    }

    public class RestResponse<T> : RestResponse
    {
        public T Data { get; set; }
    }
}