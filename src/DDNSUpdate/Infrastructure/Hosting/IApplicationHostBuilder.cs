using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Hosting;

public interface IApplicationHostBuilder
{
    IHost Build(string[] commandlineArguments);
}