using System.Net;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Common.Logging;
using Jal.ChainOfResponsability.Installer;
using Jal.HttpClient.Common.Logging;
using Jal.HttpClient.Common.Logging.Installer;
using Jal.HttpClient.Extensions;
using Jal.HttpClient.Installer;
using Jal.HttpClient.Model;
using Jal.Locator.CastleWindsor.Installer;
using Jal.RestClient.Installer;
using Jal.RestClient.Interface.Fluent;
using Jal.RestClient.Json;
using NUnit.Framework;
using Shouldly;

namespace Jal.RestClient.Tests
{
    [TestFixture]
    public class Tests
    {
        private IRestFluentHandler _restFluentHandler;

        [SetUp]
        public void Setup()
        {
            var log = LogManager.GetLogger("logger");

            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Register(Component.For<ILog>().Instance(log));

            container.Install(new HttpClientInstaller());

            container.Install(new HttpClientCommonLoggingInstaller());

            container.Install(new RestClientInstaller());

            container.Install(new ChainOfResponsabilityInstaller());

            container.Install(new ServiceLocatorInstaller());

            _restFluentHandler = container.Resolve<IRestFluentHandler>();
        }

        [Test]
        public void Get_With_ShouldNotBeNull()
        {
            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").WithMiddleware(x => x.UseCommonLogging()).Path("posts/1").Get.MapTo<Customer>().Send(new HttpIdentity("abc")))
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }

        }

        [Test]
        public void Get_WithAuthenticator_ShouldNotBeNull()
        {
            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").WithMiddleware(x=>x.AuthorizedByBasicHttp("xxx", "yyy")).Path("posts/1").Get.MapTo<Customer>().Send())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }

        }

        [Test]
        public void Get_WithStatusCode_ShouldNotBeNull()
        {
            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts/1").Get.MapTo<Customer>().When(HttpStatusCode.OK).Send())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }

        }

        [Test]
        public void GetAll_With_ShouldNotBeNull()
        {
            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts").Get.MapTo<Customer[]>().Send())
            { 

                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }

        [Test]
        public void GetAll_WithQueryParameters_ShouldNotBeNull()
        {
            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts").WithQueryParameter(x => x.Add("userId", "1")).Get.MapTo<Customer[]>().Send())
            { 
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }


        [Test]
        public void Post_With_ShouldNotBeNull()
        {
            var post = new Customer() {Body = "", Title = "", UserId = 2};

            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts").Post.Data(post).MapTo<Customer>().Send())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }

        [Test]
        public void Put_With_ShouldNotBeNull()
        {
            var post = new Customer() { Body = "", Title = "", UserId = 2, Id = 1};

            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts/1").Put.Data(post).MapTo<Customer>().When(HttpStatusCode.OK).Send())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }

        [Test]
        public void Patch_With_ShouldNotBeNull()
        {
            var post = new Customer() { Body = "", Title = "", UserId = 2, Id = 1 };

            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").WithTimeout(10000).Path("posts/1").Patch.Data(post).MapTo<Customer>().When(HttpStatusCode.OK).Send())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }

        [Test]
        public void Delete_With_ShouldNotBeNull()
        {
            using (var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts/1").Delete.Send())
            {
                response.ShouldNotBeNull();
            }
        }

        [Test]
        public void Delete1_With_ShouldNotBeNull()
        {
            var resource = "5bba5502-2cff-4f78-8fde-f898c93597f5";

            var clientid = "90d6b99c-cbb5-49be-ba3f-cc44864d73d0";

            var clientsecret = "9nrDlBlLtLDInbMqCeC1sUMS/xghTx7G0XT0QpRSOUc=";

            var body = $"resource={HttpUtility.UrlEncode(resource)}&client_id={HttpUtility.UrlEncode(clientid)}&client_secret={HttpUtility.UrlEncode(clientsecret)}&grant_type=client_credentials";

            using (var token = _restFluentHandler.Url("https://login.microsoftonline.com").Path("dd5d5cfe-d892-4892-b623-1134653cc289/oauth2/token").Post.Data(body, "application/x-www-form-urlencoded").MapTo<AccessToken>().When(HttpStatusCode.OK).Send())
            {
                using (var response = _restFluentHandler.Url("http://cuy-api-qa.cignium-cuy-qa-ase.p.azurewebsites.net").WithMiddleware(x=>x.AuthorizedByBearerToken(token.Data.Access_Token)).Path("api/v1/campaigns/1").Get.MapTo<Campaign>().Send())
                {
                    response.ShouldNotBeNull();
                } 
            }
        }
    }



    public class AccessToken
    {
        public string Access_Token { get; set; }

        public string Token_Type { get; set; }

        public int Expires_In { get; set; }
    }
    public class Campaign
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CampaignDialerId { get; set; }
    }
}
