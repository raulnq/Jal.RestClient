using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.RestClient.Impl;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Installer
{
    public class TokenAutenticatorInstaller : IWindsorInstaller
    {
        private readonly string _authenticatorName;

        private readonly string _tokenName;

        public TokenAutenticatorInstaller(string authenticatorName, string tokenName = "Bearer")
        {
            _authenticatorName = authenticatorName;
            _tokenName = tokenName;
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IAuthenticator>().ImplementedBy<TokenAuthenticator>().Named(_authenticatorName)
                .DependsOn(new
                 {
                     tokenName = _tokenName,
                 })
                );
        }
    }
}