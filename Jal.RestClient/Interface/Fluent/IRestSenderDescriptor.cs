using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestSenderDescriptor<T>
    {
        RestResponse<T> Send(HttpIdentity httpIdentity=null);

        Task<RestResponse<T>> SendAsync(HttpIdentity httpIdentity=null);
    }

    public interface IRestSenderDescriptor
    {
        RestResponse Send(HttpIdentity httpIdentity= null);

        Task<RestResponse> SendAsync(HttpIdentity httpIdentity= null);
    }
}