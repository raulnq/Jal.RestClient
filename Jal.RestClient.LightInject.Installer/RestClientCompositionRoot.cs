using LightInject;
using System.Linq;

namespace Jal.RestClient.LightInject.Installer
{
    public class RestClientCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            if (serviceRegistry.AvailableServices.All(x => x.ServiceType != typeof(IRestFluentHandler)))
            {
                serviceRegistry.Register<IRestFluentHandler, RestFluentHandler>(new PerContainerLifetime());
            } 
        }
    }
}
