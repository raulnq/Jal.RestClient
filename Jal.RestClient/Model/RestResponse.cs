using System;
using Jal.HttpClient.Model;

namespace Jal.RestClient.Model
{
    public class RestResponse : IDisposable
    {
        public RestResponse(HttpResponse httpresponse)
        {
            HttpResponse = httpresponse;
        }
        public HttpResponse HttpResponse { get; internal set; }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(HttpResponse!=null)
                {
                    HttpResponse.Dispose();
                }
                HttpResponse = null;
            }
        }
    }

    public class RestResponse<T> : RestResponse
    {
        public RestResponse(HttpResponse httpresponse, T data):base(httpresponse)
        {
            Data = data;
        }
        public T Data { get; }
    }
}