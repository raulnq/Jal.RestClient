using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Jal.RestClient.Installer
{
    public class RestClientInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (!container.Kernel.HasComponent(typeof(IRestFluentHandler)))
            {
                container.Register(Component.For<IRestFluentHandler>().ImplementedBy<RestFluentHandler>());
            }
        }
    }
}