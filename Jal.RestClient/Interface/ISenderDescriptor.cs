using System;
using System.Net;
using System.Threading.Tasks;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface
{
    public interface ISenderDescriptor
    {
        RestResponse Send();

        Task<RestResponse> SendAsync();

        RestResponse<T> Send<T>(Func<string, T> converter, HttpStatusCode[] successfulStatusCodes);

        Task<RestResponse> SendAsync<T>(Func<string, T> converter, HttpStatusCode[] successfulStatusCodes);

    }
}