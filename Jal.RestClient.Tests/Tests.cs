using System;
using System.Net;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Common.Logging;
using Jal.HttpClient.Common.Logging;
using Jal.HttpClient.Common.Logging.Installer;
using Jal.HttpClient;
using Jal.RestClient.Installer;
using Jal.RestClient.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Jal.RestClient.Tests
{
    [TestClass]
    public class Tests
    {
        private IRestFluentHandler _restFluentHandler;

        [TestInitialize]
        public void Setup()
        {
            var log = LogManager.GetLogger("logger");

            var container = new WindsorContainer();

            container.Register(Component.For<ILog>().Instance(log));

            container.AddRestClient(c=> { c.AddCommonLoggingForHttpClient(); });

            _restFluentHandler = container.GetRestClient();
        }

        [TestMethod]
        public async Task Get_With_ShouldNotBeNull()
        {
            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com").WithMiddleware(x => x.UseCommonLogging()).Path("posts/1").Get.MapTo<Customer>().WithTracing(new HttpTracingContext("abc", null, null)).SendAsync())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }

        }

        [TestMethod]
        public async Task Get_WithAuthenticator_ShouldNotBeNull()
        {
            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com").WithMiddleware(x=>x.AuthorizedByBasicHttp("xxx", "yyy")).Path("posts/1").Get.MapTo<Customer>().SendAsync())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }

        }

        [TestMethod]
        public async Task Get_WithStatusCode_ShouldNotBeNull()
        {
            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com").Path("posts/1").Get.MapTo<Customer>().When(HttpStatusCode.OK).SendAsync())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }

        }

        [TestMethod]
        public async Task GetAll_With_ShouldNotBeNull()
        {
            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com").Path("posts").Get.MapTo<Customer[]>().SendAsync())
            { 

                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }

        [TestMethod]
        public async Task GetAll_WithQueryParameters_ShouldNotBeNull()
        {
            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com").Path("posts").WithQueryParameter(x => x.Add("userId", "1")).Get.MapTo<Customer[]>().SendAsync())
            { 
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }


        [TestMethod]
        public async Task Post_With_ShouldNotBeNull()
        {
            var post = new Customer() {Body = "", Title = "", UserId = 2};

            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com").Path("posts").Post.Data(post).MapTo<Customer>().SendAsync())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }

        [TestMethod]
        public async Task Put_With_ShouldNotBeNull()
        {
            var post = new Customer() { Body = "", Title = "", UserId = 2, Id = 1};

            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com").Path("posts/1").Put.Data(post).MapTo<Customer>().When(HttpStatusCode.OK).SendAsync())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }

        [TestMethod]
        public async Task Patch_With_ShouldNotBeNull()
        {
            var post = new Customer() { Body = "", Title = "", UserId = 2, Id = 1 };

            var client = new System.Net.Http.HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(10000)
            };

            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com", client).Path("posts/1").Patch.Data(post).MapTo<Customer>().When(HttpStatusCode.OK).SendAsync())
            {
                response.ShouldNotBeNull();

                response.Data.ShouldNotBeNull();
            }
        }

        [TestMethod]
        public async Task Delete_With_ShouldNotBeNull()
        {
            var post = new Customer() { Body = "", Title = "", UserId = 2, Id = 1 };

            using (var response = await _restFluentHandler.Uri("https://jsonplaceholder.typicode.com").Path("posts/1").Delete.Data(post).SendAsync())
            {
                response.ShouldNotBeNull();
            }
        }
    }
}
