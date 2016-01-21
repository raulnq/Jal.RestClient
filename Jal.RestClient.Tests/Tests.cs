using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Jal.Converter.Installer;
using Jal.HttpClient.Installer;
using Jal.HttpClient.Interface;
using Jal.HttpClient.Model;
using Jal.Locator.CastleWindsor.Installer;
using Jal.RestClient.Installer;
using Jal.RestClient.Interface;
using Jal.RestClient.Model;
using NUnit.Framework;

namespace Jal.RestClient.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Get_List_Successful()
        {
            AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(TestContext.CurrentContext.TestDirectory);

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new HttpClientInstaller());

            container.Install(new ConverterInstaller());

            container.Install(new RestClientInstaller("Example", true));

            var restHandler = container.Resolve<IRestHandler>();

            var response = restHandler.Get<Customer[]>("http://www.thomas-bayer.com/sqlrest/CUSTOMER/");
        }

        [Test]
        public void Get_Item_Successful()
        {
            AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(TestContext.CurrentContext.TestDirectory);

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new HttpClientInstaller());

            container.Install(new ConverterInstaller());

            container.Install(new RestClientInstaller("Example", true));

            var restHandler = container.Resolve<IRestHandler>();

            var response = restHandler.Get<Customer>("http://www.thomas-bayer.com/sqlrest/CUSTOMER/2");
        }

        [Test]
        public void Post_Item_Successful()
        {
            AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(TestContext.CurrentContext.TestDirectory);

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new HttpClientInstaller());

            container.Install(new ConverterInstaller());

            container.Install(new RestClientInstaller("Example", true));

            var restHandler = container.Resolve<IRestHandler>();

            var response = restHandler.Post("http://www.thomas-bayer.com/sqlrest/CUSTOMER/", new Customer(), HttpContentType.Xml);
        }
    }
}
