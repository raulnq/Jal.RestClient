using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.RestClient.Impl;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Installer
{
    public class RestClientInstaller : IWindsorInstaller
    {

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IRestHandler>().ImplementedBy<RestHandler>());
            container.Register(Component.For<IRestHandlerBuilder>().ImplementedBy<RestHandlerBuilder>());
            
        }
    }
}