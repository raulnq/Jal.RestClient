using Jal.RestClient.Impl;

namespace Jal.RestClient.Interface
{
    public interface IAuthenticatorDescriptor : ISenderDescriptor
    {
        ISenderDescriptor WithAuthenticator(IAuthenticator authenticator);
    }
}