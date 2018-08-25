using System;
using Jal.HttpClient.Interface.Fluent;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestMiddlewareDescriptor : IRestHeaderDescriptor
    {
        IRestHeaderDescriptor WithMiddlewares(Action<IHttpMiddlewareDescriptor> action);
    }
}