using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestSenderDescriptor<T>
    {
        Task<RestResponse<T>> SendAsync(HttpIdentity identity=null);
    }

    public interface IRestSenderDescriptor
    {
        Task<RestResponse> SendAsync(HttpIdentity identity = null);
    }
}