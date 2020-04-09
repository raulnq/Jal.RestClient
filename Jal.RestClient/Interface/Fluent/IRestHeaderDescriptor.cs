using System;
using Jal.HttpClient;

namespace Jal.RestClient
{
    public interface IRestHeaderDescriptor : IRestResourceDescriptor
    {
        IRestResourceDescriptor WithHeader(Action<IHttpHeaderDescriptor> action);
    }
}