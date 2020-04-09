using System.Net;

namespace Jal.RestClient
{
    public interface IRestWhenDescriptor<T> : IRestSenderDescriptor<T>
    {
        IRestSenderDescriptor<T> When(HttpStatusCode statuscode);

        IRestSenderDescriptor<T> Always();
    }
}