using System;

namespace Jal.RestClient.Interface
{
    public interface IContentDescriptor : IAuthenticatorDescriptor
    {
        IContentDescriptor WithContentType(string contentType);

        IContentDescriptor WithContent(string content);

        IContentDescriptor WithContent<T>(T content, Func<T,string> converter);
    }
}