using Jal.HttpClient.Interface;
using Jal.RestClient.Impl.Fluent;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestFluentHandlerStartBuilder
    {
        IRestFluentHandlerEndBuilder UseHttpHandler(IHttpHandler httpHandler);
    }
}