using System;

namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestContentDescriptor
    {
        IRestMapDescriptor Data<T>(T data, Func<T, string> converter, string contentType = "application/json", string characterSet = "charset=UTF-8");

        IRestMapDescriptor Data(string data, string contentType = "application/json", string characterSet = "charset=UTF-8");
    }
}