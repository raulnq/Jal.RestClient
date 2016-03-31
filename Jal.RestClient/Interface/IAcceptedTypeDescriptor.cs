using System;

namespace Jal.RestClient.Interface
{
    public interface IAcceptedTypeDescriptor : IAuthenticatorDescriptor
    {
        IAuthenticatorDescriptor WithAcceptedType(string acceptedType);
    }
}