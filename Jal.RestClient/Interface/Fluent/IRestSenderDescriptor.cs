using System.Threading.Tasks;
using Jal.RestClient.Model;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestSenderDescriptor<T>
    {
        RestResponse<T> Send { get; }

        Task<RestResponse<T>> SendAsync();
    }

    public interface IRestSenderDescriptor
    {
        RestResponse Send { get; }

        Task<RestResponse> SendAsync();
    }
}