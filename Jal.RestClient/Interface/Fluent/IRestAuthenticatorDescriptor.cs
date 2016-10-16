using System;
using Jal.HttpClient.Model;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestAuthenticatorDescriptor : IRestHeaderDescriptor
    {
        IRestHeaderDescriptor AuthorizedBy(Action<HttpRequest> authenticator);
    }
}