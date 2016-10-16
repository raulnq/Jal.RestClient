namespace Jal.RestClient.Interface.Fluent
{
    public interface IRestVerbDescriptor
    {
        IRestMapDescriptor Get { get; }

        IRestMapDescriptor Delete { get; }

        IRestContentDescriptor Post { get; }

        IRestContentDescriptor Put { get; }

        IRestContentDescriptor Patch { get; }
    }
}