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
    public class RestHandlerTests
    {
        [Test]
        public void Get_List_Successful()
        {
            var container = new WindsorContainer();

            container.Install(new HttpClientInstaller());

            container.Install(new RestClientInstaller());

            var restHandler = container.Resolve<IRestHandler>();

            var response = restHandler.Get("http://www.thomas-bayer.com/sqlrest/CUSTOMER/2");
        }

        [Test]
        public void Get_Item_Successful()
        {
            var container = new WindsorContainer();

            container.Install(new HttpClientInstaller());

            container.Install(new RestClientInstaller());

            var restHandler = container.Resolve<IRestHandler>();

            var response = restHandler.Get("http://www.thomas-bayer.com/sqlrest/CUSTOMER/");

        }

        [Test]
        public void Post_Item_Successful()
        {
            var container = new WindsorContainer();

            container.Install(new HttpClientInstaller());

            container.Install(new RestClientInstaller());

            var restHandler = container.Resolve<IRestHandler>();

            var response = restHandler.Post("http://www.thomas-bayer.com/sqlrest/CUSTOMER/", 
@"<CUSTOMER xmlns:xlink=""://www.w3.org/1999/xlink""><ID>159753</ID><FIRSTNAME>COMPUWARE</FIRSTNAME><LASTNAME>CHA_DEV</LASTNAME><STREET>429 Seventh Av 4444.</STREET><CITY>BEIJING-PANGU</CITY></CUSTOMER>");
        }
    }
}
