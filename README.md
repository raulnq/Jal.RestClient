# Jal.RestClient [![NuGet](https://img.shields.io/nuget/v/Jal.RestClient.svg)](https://www.nuget.org/packages/Jal.RestClient)
Just another library to call REST APIs
## How to use?

```csharp

using (var response = await client.Uri("https://jsonplaceholder.typicode.com")
	.Path("posts/1")
	.Get.MapTo<Customer>().When(HttpStatusCode.OK)
	.SendAsync())
{

}

using (var response = await client.Uri("https://jsonplaceholder.typicode.com")
	.Path("posts")
	.WithQueryParameter(x => x.Add("userId", "1"))
	.Get.MapTo<Customer[]>()
	.SendAsync())
{ 

}

var post = new Customer() {Body = "", Title = "", UserId = 2};

using (var response = await client.Uri("https://jsonplaceholder.typicode.com")
	.Path("posts")
	.Post.Data(post).MapTo<Customer>()
	.SendAsync())
{

}

var post = new Customer() { Body = "", Title = "", UserId = 2, Id = 1};

using (var response = await client.Uri("https://jsonplaceholder.typicode.com")
	.Path("posts/1")
	.Put.Data(post).MapTo<Customer>().When(HttpStatusCode.OK)
	.SendAsync())
{

}

var post = new Customer() { Body = "", Title = "", UserId = 2, Id = 1 };

using (var response = await client.Uri("https://jsonplaceholder.typicode.com")
	.Path("posts/1")
	.Delete.Data(post)
	.SendAsync())
{

}
```
## IRestFluentHandler interface building
### Castle Windsor [![NuGet](https://img.shields.io/nuget/v/Jal.RestClient.Installer.svg)](https://www.nuget.org/packages/Jal.RestClient.Installer)
```csharp
var container = new WindsorContainer();

container.AddRestClient();

var client = container.GetRestClient();
```
### LightInject [![NuGet](https://img.shields.io/nuget/v/Jal.RestClient.LightInject.Installer.svg)](https://www.nuget.org/packages/Jal.RestClient.LightInject.Installer)
```csharp
var container = new ServiceContainer();

container.AddRestClient();

var client = container.GetRestClient();
```
### Microsoft.Extensions.DependencyInjection [![NuGet](https://img.shields.io/nuget/v/Jal.RestClient.Microsoft.Extensions.DependencyInjection.Installer.svg)](https://www.nuget.org/packages/Jal.RestClient.Microsoft.Extensions.DependencyInjection.Installer)
```csharp
var container = new ServiceCollection();

container.AddRestClient();

var provider = container.BuildServiceProvider();

var client = provider.GetRestClient();
```
## Middlewares

Use the same middlewares used in [Jal.HttpClient](https://github.com/raulnq/Jal.HttpClient)