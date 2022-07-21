using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Hosting;

public interface ILoggingConfigurator
{
    void Configure(IHostBuilder hostBuilder);
}