using Jal.RestClient.Impl.Fluent;
using Jal.RestClient.Interface.Fluent;
using Microsoft.Extensions.DependencyInjection;

namespace Jal.RestClient.Microsoft.Extensions.DependencyInjection.Installer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestClient(this IServiceCollection servicecollection)
        {
            servicecollection.AddSingleton<IRestFluentHandler, RestFluentHandler>();

            return servicecollection;
        }
    }
}
