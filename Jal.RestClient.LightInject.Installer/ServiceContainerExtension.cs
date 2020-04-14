using Jal.HttpClient;
using Jal.HttpClient.LightInject.Installer;
using LightInject;
using System;

namespace Jal.RestClient.LightInject.Installer
{
    public static class ServiceContainerExtension
    {
        public static void AddRestClient(this IServiceContainer container, Action<IHttpClientBuilder> action = null)
        {
            container.AddHttpClient(action);

            container.RegisterFrom<RestClientCompositionRoot>();
        }

        public static IRestFluentHandler GetRestClient(this IServiceContainer container)
        {
            return container.GetInstance<IRestFluentHandler>();
        }
    }
}
