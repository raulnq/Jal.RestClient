using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestSenderDescriptor<T>
    {
        Task<RestResponse<T>> SendAsync();

        IRestSenderDescriptor<T> WithIdentity(HttpIdentity identity);
    }

    public interface IRestSenderDescriptor
    {
        Task<RestResponse> SendAsync();

        IRestSenderDescriptor WithIdentity(HttpIdentity identity);
    }
}