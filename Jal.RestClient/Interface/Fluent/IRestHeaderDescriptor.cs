using System;
using Jal.HttpClient.Interface.Fluent;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestHeaderDescriptor : IRestResourceDescriptor
    {
        IRestResourceDescriptor WithHeader(Action<IHttpHeaderDescriptor> action);
    }
}