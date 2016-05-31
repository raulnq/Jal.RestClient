using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Jal.Converter.Installer;
using Jal.Converter.Interface;
using Jal.HttpClient.Installer;
using Jal.HttpClient.Interface;
using Jal.HttpClient.Model;
using Jal.Locator.CastleWindsor.Installer;
using Jal.RestClient.Fluent;
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
            AssemblyFinder.Impl.AssemblyFinder.Current = AssemblyFinder.Impl.AssemblyFinder.Builder.UsePath(TestContext.CurrentContext.TestDirectory).Create;

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new HttpClientInstaller());

            container.Install(new ConverterInstaller(() => AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("Converter")));

            container.Install(new RestClientInstaller());

            var modelConverter = container.Resolve<IModelConverter>();

            var restHandler = container.Resolve<IRestHandlerBuilder>();

            var response = restHandler.Get("http://www.thomas-bayer.com/sqlrest/CUSTOMER/2")
                .Send(x => modelConverter.Convert<string, Customer>(x), new []{HttpStatusCode.OK});
        }

        [Test]
        public void Get_Item_Successful()
        {
            AssemblyFinder.Impl.AssemblyFinder.Current = AssemblyFinder.Impl.AssemblyFinder.Builder.UsePath(TestContext.CurrentContext.TestDirectory).Create;

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new HttpClientInstaller());

            container.Install(new ConverterInstaller(() => AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("Converter")));

            container.Install(new RestClientInstaller());

            var restHandler = container.Resolve<IRestHandlerBuilder>();

            var modelConverter = container.Resolve<IModelConverter>();

            var response = restHandler.Get("http://www.thomas-bayer.com/sqlrest/CUSTOMER/")
                .Send(x => modelConverter.Convert<string, Customer[]>(x), new[] { HttpStatusCode.OK });

        }

        [Test]
        public void GetSimple_Item_Successful()
        {
            AssemblyFinder.Impl.AssemblyFinder.Current = AssemblyFinder.Impl.AssemblyFinder.Builder.UsePath(TestContext.CurrentContext.TestDirectory).Create;

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new HttpClientInstaller());

            container.Install(new ConverterInstaller(() => AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("Converter")));

            container.Install(new RestClientInstaller());

            var restHandler = container.Resolve<IRestHandlerBuilder>();

            var response = restHandler.Get("http://www.thomas-bayer.com/sqlrest/CUSTOMER/").Send();
        }

        [Test]
        public void Post_Item_Successful()
        {
            AssemblyFinder.Impl.AssemblyFinder.Current = AssemblyFinder.Impl.AssemblyFinder.Builder.UsePath(TestContext.CurrentContext.TestDirectory).Create;

            var container = new WindsorContainer();

            container.Install(new ServiceLocatorInstaller());

            container.Install(new HttpClientInstaller());

            container.Install(new ConverterInstaller(() => AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("Converter")));

            container.Install(new RestClientInstaller());

            var restHandler = container.Resolve<IRestHandlerBuilder>();

             var modelConverter = container.Resolve<IModelConverter>();

            var response = restHandler
                .Post("http://www.thomas-bayer.com/sqlrest/CUSTOMER/")
                .Xml(new Customer(), x => modelConverter.Convert<Customer, string>(x))
                .Send();
        }
    }
}
