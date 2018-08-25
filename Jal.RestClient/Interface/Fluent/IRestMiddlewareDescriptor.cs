using System;
using Jal.HttpClient.Interface.Fluent;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestMiddlewareDescriptor : IRestHeaderDescriptor
    {
        IRestHeaderDescriptor WithMiddleware(Action<IHttpMiddlewareDescriptor> action);

        IRestHeaderDescriptor WithContext(Action<IHttpDataDescriptor> action);
    }
}