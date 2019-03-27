using System;
using Jal.HttpClient.Interface.Fluent;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestQueryParameteDescriptor : IRestVerbDescriptor
    {
        IRestVerbDescriptor WithQueryParameter(Action<IHttpQueryParameterDescriptor> action);

    }
}