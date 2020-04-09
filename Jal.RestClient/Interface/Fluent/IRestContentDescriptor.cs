using System;

namespace Jal.RestClient
{
    public interface IRestContentDescriptor : IRestSenderDescriptor
    {
        IRestMapDescriptor Data<T>(T data, Func<T, string> converter, string contentType = "application/json");

        IRestMapDescriptor Data(string data, string contentType = "application/json");
    }
}