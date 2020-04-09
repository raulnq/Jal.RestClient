using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jal.RestClient.Microsoft.Extensions.DependencyInjection.Installer
{
    public static class ServiceProviderExtensions
    {
        public static IRestFluentHandler GetRestClient(this IServiceProvider provider)
        {
            return provider.GetService<IRestFluentHandler>();
        }
    }
}
