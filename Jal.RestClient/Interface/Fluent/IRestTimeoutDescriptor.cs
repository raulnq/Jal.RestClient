namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestTimeoutDescriptor : IRestResourceDescriptor
    {
        IRestResourceDescriptor WithTimeout(int timeout);
    }
}