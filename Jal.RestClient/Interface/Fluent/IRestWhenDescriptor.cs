using System.Net;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestWhenDescriptor<T> : IRestSenderDescriptor<T>
    {
        IRestSenderDescriptor<T> When(HttpStatusCode httpStatusCode);

        IRestSenderDescriptor<T> Always();
    }
}