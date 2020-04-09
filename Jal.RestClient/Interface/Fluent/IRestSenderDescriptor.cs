using System.Threading.Tasks;
using Jal.HttpClient;

namespace Jal.RestClient
{
    public interface IRestSenderDescriptor<T>
    {
        Task<RestResponse<T>> SendAsync();

        IRestSenderDescriptor<T> WithTracing(HttpTracingContext context);
    }

    public interface IRestSenderDescriptor
    {
        Task<RestResponse> SendAsync();

        IRestSenderDescriptor WithTracing(HttpTracingContext context);
    }
}