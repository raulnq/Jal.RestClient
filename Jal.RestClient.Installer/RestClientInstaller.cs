using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.RestClient.Impl;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Installer
{
    public class RestClientInstaller : IWindsorInstaller
    {
        private readonly string _restClientName;

        private readonly string _authenticatorName;

        private readonly bool _default;

        public RestClientInstaller(string restClientName, bool @default = false, string authenticatorName = null)
        {
            _restClientName = restClientName;

            _default = @default;

            _authenticatorName = authenticatorName;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (!string.IsNullOrWhiteSpace(_authenticatorName))
            {
                if (_default)
                {
                    container.Register(Component.For<IRestHandler>().ImplementedBy<RestHandler>().Named(_restClientName).DependsOn(ServiceOverride.ForKey<IAuthenticator>().Eq(_authenticatorName)).IsDefault());   
                }
                else
                {
                    container.Register(Component.For<IRestHandler>().ImplementedBy<RestHandler>().Named(_restClientName).DependsOn(ServiceOverride.ForKey<IAuthenticator>().Eq(_authenticatorName)));   
                }
                
            }
            else
            {
                if (_default)
                {
                    container.Register(Component.For<IRestHandler>().ImplementedBy<RestHandler>().Named(_restClientName).IsDefault()); 
                }
                else
                {
                    container.Register(Component.For<IRestHandler>().ImplementedBy<RestHandler>().Named(_restClientName)); 
                }
                
            }


        }
    }
}