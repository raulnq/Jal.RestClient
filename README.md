# Jal.RestClient
Just another library to call REST web services

Note: The Jal.HttpClient, Jal.Converter, Jal.Locator.CastleWindsor and Jal.AssemblyFinder library are needed.

Setup the Jal.AssemblyFinder library

    var directory = AppDomain.CurrentDomain.BaseDirectory;
    AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(directory);
    
Setup the Castle Windsor container

    var container = new WindsorContainer();
    
Install the Jal.Locator.CastleWindsor library    

    container.Install(new ServiceLocatorInstaller());
    
Install the Jal.Converter library

    container.Install(new ConverterInstaller());
    
Install the Jal.HttpClient library

    container.Install(new HttpClientInstaller());
    
Install the Jal.RestClient library

    container.Install(new RestClientInstaller("Example, true"));
    
Resolve an instance of the IRestHandler class

    var restHandler = container.Resolve<IRestHandler>();
    
Create your converter class

    public class StringToCustomersConverter : AbstractConverter<string, Customer[]>
    {
        public override Customer[] Convert(string source)
        {
            return new []{new Customer()};
        }
    }
    
Tag the assembly container of the converter classes in order to be read by the library

    [assembly: AssemblyTag("Converter")]
    
Send a request to http://www.thomas-bayer.com/sqlrest/CUSTOMER/

    var response = restHandler.Get<Customer[]>("http://www.thomas-bayer.com/sqlrest/CUSTOMER/");
