namespace Jal.RestClient
{
    public interface IRestResourceDescriptor
    {
        IRestQueryParameteDescriptor Path(string path);
    }
}