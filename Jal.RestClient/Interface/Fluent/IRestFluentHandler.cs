namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestFluentHandler
    {
        IRestMiddlewareDescriptor Url(string url);
    }
}