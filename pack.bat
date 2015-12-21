packages\NuGet.CommandLine.3.3.0\tools\nuget pack Jal.RestClient\Jal.RestClient.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.RestClient.Nuget

packages\NuGet.CommandLine.3.3.0\tools\nuget pack Jal.RestClient.Installer\Jal.RestClient.Installer.csproj -Properties "Configuration=Release;Platform=AnyCPU;OutputPath=bin\Release" -Build -IncludeReferencedProjects -OutputDirectory Jal.RestClient.Nuget

pause;