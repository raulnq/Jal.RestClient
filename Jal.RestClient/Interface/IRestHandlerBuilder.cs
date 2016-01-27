using Jal.RestClient.Impl;

namespace Jal.RestClient.Interface
{
    public interface IRestHandlerBuilder
    {
        IAuthenticatorDescriptor Get(string url);

        IContentDescriptor Post(string url);

        IContentDescriptor Put(string url);

        IAuthenticatorDescriptor Delete(string url);
    }

}