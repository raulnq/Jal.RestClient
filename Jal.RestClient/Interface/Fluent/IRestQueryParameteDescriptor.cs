using System;
using Jal.HttpClient;

namespace Jal.RestClient
{
    public interface IRestQueryParameteDescriptor : IRestVerbDescriptor
    {
        IRestVerbDescriptor WithQueryParameter(Action<IHttpQueryParameterDescriptor> action);

    }
}