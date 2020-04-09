using System;
using Jal.HttpClient;

namespace Jal.RestClient
{
    public interface IRestMiddlewareDescriptor : IRestHeaderDescriptor
    {
        IRestHeaderDescriptor WithMiddleware(Action<IHttpMiddlewareDescriptor> action);
    }
}