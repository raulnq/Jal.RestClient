using Jal.HttpClient.Interface;
using Jal.RestClient.Impl;
using Jal.RestClient.Impl.Fluent;
using Jal.RestClient.Interface.Fluent;
using LightInject;

namespace Jal.RestClient.LightInject.Installer
{
    public class RestClientCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IRestFluentHandler, RestFluentHandler>( new PerContainerLifetime());
            serviceRegistry.Register<IHttpMiddleware, BasicHttpAuthenticatorHttpMiddleware>(typeof(BasicHttpAuthenticatorHttpMiddleware).FullName, new PerContainerLifetime());
            serviceRegistry.Register<IHttpMiddleware, TokenAuthenticatorHttpMiddleware>(typeof(TokenAuthenticatorHttpMiddleware).FullName, new PerContainerLifetime());
        }
    }
}
