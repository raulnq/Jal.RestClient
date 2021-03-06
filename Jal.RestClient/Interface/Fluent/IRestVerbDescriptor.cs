namespace Jal.RestClient
{
    public interface IRestVerbDescriptor
    {
        IRestMapDescriptor Get { get; }

        IRestContentDescriptor Delete { get; }

        IRestContentDescriptor Post { get; }

        IRestContentDescriptor Put { get; }

        IRestContentDescriptor Patch { get; }
    }
}