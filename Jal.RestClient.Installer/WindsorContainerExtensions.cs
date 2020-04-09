using Castle.Windsor;
using Jal.HttpClient.Installer;
using System;

namespace Jal.RestClient.Installer
{
    public static class WindsorContainerExtensions
    {
        public static void AddRestClient(this IWindsorContainer container, Action<IWindsorContainer> action = null)
        {
            container.AddHttpClient(action);

            container.Install(new RestClientInstaller());
        }

        public static IRestFluentHandler GetRestClient(this IWindsorContainer container)
        {
            return container.Resolve<IRestFluentHandler>();
        }
    }
}