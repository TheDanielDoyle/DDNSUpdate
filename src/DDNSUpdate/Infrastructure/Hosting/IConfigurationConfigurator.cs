using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Hosting;

public interface IConfigurationConfigurator
{
    void Configure(IHostBuilder hostBuilder, string[] commandlineArguments);
}