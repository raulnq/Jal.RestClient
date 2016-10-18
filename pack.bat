packages\NuGet.CommandLine.3.4.4-rtm-final\tools\nuget pack Jal.RestClient\Jal.RestClient.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.RestClient.Nuget

packages\NuGet.CommandLine.3.4.4-rtm-final\tools\nuget pack Jal.RestClient.Installer\Jal.RestClient.Installer.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.RestClient.Nuget

packages\NuGet.CommandLine.3.4.4-rtm-final\tools\nuget pack Jal.RestClient.Json\Jal.RestClient.Json.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.RestClient.Nuget

packages\NuGet.CommandLine.3.4.4-rtm-final\tools\nuget pack Jal.RestClient.LightInject.Installer\Jal.RestClient.LightInject.Installer.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.RestClient.Nuget


pause;