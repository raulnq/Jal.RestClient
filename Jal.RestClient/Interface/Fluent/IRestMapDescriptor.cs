using System;

namespace Jal.RestClient
{
    public interface IRestMapDescriptor : IRestSenderDescriptor
    {
        IRestWhenDescriptor<T> MapTo<T>(Func<string, T> converter, string acceptedType = "application/json");
    }
}