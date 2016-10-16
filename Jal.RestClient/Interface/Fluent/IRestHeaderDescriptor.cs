using System;
using Jal.HttpClient.Interface.Fluent;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestHeaderDescriptor : IRestTimeoutDescriptor
    {
        IRestTimeoutDescriptor WithHeader(Action<IHeaderDescriptor> headerDescriptorAction);
    }
}