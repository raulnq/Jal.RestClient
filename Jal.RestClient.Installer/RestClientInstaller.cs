using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.HttpClient.Interface;
using Jal.RestClient.Impl;
using Jal.RestClient.Impl.Fluent;
using Jal.RestClient.Interface.Fluent;

namespace Jal.RestClient.Installer
{
    public class RestClientInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRestFluentHandler>().ImplementedBy<RestFluentHandler>());
            container.Register(Component.For<IHttpMiddleware>().ImplementedBy<BasicHttpAuthenticatorHttpMiddleware>().Named(typeof(BasicHttpAuthenticatorHttpMiddleware).FullName));
            container.Register(Component.For<IHttpMiddleware>().ImplementedBy<TokenAuthenticatorHttpMiddleware>().Named(typeof(TokenAuthenticatorHttpMiddleware).FullName));
        }
    }
}