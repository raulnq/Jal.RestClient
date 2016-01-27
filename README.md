# Jal.RestClient
Just another library to call REST web services

Note: The Jal.HttpClient library is needed.
    
Setup the Castle Windsor container

    var container = new WindsorContainer();
    
Install the Jal.HttpClient library

    container.Install(new HttpClientInstaller());
    
Install the Jal.RestClient library

    container.Install(new RestClientInstaller());
    
Resolve an instance of the IRestHandler class

    var restHandler = container.Resolve<IRestHandler>();
    
Send a request to http://www.thomas-bayer.com/sqlrest/CUSTOMER/

    var response = restHandler.Get("http://www.thomas-bayer.com/sqlrest/CUSTOMER/");
