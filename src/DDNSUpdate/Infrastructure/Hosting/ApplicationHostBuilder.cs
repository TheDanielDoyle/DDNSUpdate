using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Hosting
{
    public class ApplicationHostBuilder : IApplicationHostBuilder
    {
        private readonly IConfigurationConfigurator _configurationConfigurator;
        private readonly ILoggingConfigurator _loggingConfigurator;

        public ApplicationHostBuilder(IConfigurationConfigurator configurationConfigurator, ILoggingConfigurator loggingConfigurator)
        {
            _configurationConfigurator = configurationConfigurator;
            _loggingConfigurator = loggingConfigurator;
        }

        public IHost Build(string[] commandlineArguments)
        {
            IHostBuilder hostBuilder = Host
                .CreateDefaultBuilder()
                .UseConsoleLifetime();

            _configurationConfigurator.Configure(hostBuilder, commandlineArguments);
            _loggingConfigurator.Configure(hostBuilder);

            hostBuilder
                .ConfigureServicesWithProfiles()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory());

            return hostBuilder.Build();
        }
    }
}
