using Jal.HttpClient.Microsoft.Extensions.DependencyInjection.Installer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Jal.RestClient.Microsoft.Extensions.DependencyInjection.Installer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRestClient(this IServiceCollection servicecollection, Action<IServiceCollection> action = null)
        {
            servicecollection.AddHttpClient(action);

            servicecollection.TryAddSingleton<IRestFluentHandler, RestFluentHandler>();

            return servicecollection;
        }
    }
}
