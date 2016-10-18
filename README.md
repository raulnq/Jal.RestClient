# Jal.RestClient

Just another library to call REST web services

Note: The Jal.HttpClient library is needed.
    
Setup the Castle Windsor container

    var container = new WindsorContainer();
    
Install the Jal.HttpClient library. The Jal.HttpClient.Installer is needed (you can use as well the LightInjecy version).

    container.Install(new HttpClientInstaller());
    
Install the Jal.RestClient library. The Jal.RestClient.Installer is needed  (you can use as well the LightInjecy version).

    container.Install(new RestClientInstaller());
    
Resolve an instance of the IRestHandler class

    var restHandler = container.Resolve<IRestFluentHandler>();

Use the Json serializer extension methods Jal.RestClient.Json

Send requests to https://jsonplaceholder.typicode.com

    var response= _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts/1").Get.MapTo<Post>().Send;

    var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts").WithQueryParameter(x=>x.Add("userId","1")).Get.MapTo<Post[]>().Send;

    var post = new Customer() {Body = "", Title = "", UserId = 2};

    var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts").Post.Data(post).MapTo<Post>().Send;

    var post = new Customer() { Body = "", Title = "", UserId = 2, Id = 1};

    var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts/1").Put.Data(post).MapTo<Post>().Send;

    var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").Path("posts/1").Delete.Send;

    var response = _restFluentHandler.Url("https://jsonplaceholder.typicode.com").AuthorizedByBasicHttp("user","password").Path("posts/1").Get.MapTo<Post>().Send;