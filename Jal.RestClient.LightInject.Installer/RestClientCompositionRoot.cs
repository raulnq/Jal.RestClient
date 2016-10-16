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
        }
    }
}
