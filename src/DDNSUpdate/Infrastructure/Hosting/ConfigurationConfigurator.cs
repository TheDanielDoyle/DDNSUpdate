using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DDNSUpdate.Infrastructure.Hosting
{
    public class ConfigurationConfigurator : IConfigurationConfigurator
    {
        private static readonly string _configurationFilename = "config";

        public void Configure(IHostBuilder hostBuilder, string[] commandlineArguments)
        {
            hostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                IHostEnvironment environment = context.HostingEnvironment;
                builder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"{_configurationFilename}.json", optional: true, reloadOnChange: true)
                    .AddYamlFile($"{_configurationFilename}.yaml", optional: true, reloadOnChange: true)
                    .AddYamlFile($"{_configurationFilename}.yml", optional: true, reloadOnChange: true);
                if (environment.IsDevelopment())
                {
                    builder.AddJsonFile($"{_configurationFilename}.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    builder.AddYamlFile($"{_configurationFilename}.{environment.EnvironmentName}.yaml", optional: true, reloadOnChange: true);
                    builder.AddYamlFile($"{_configurationFilename}.{environment.EnvironmentName}.yml", optional: true, reloadOnChange: true);
                }

                builder.AddEnvironmentVariables();

                if (commandlineArguments != null)
                {
                    builder.AddCommandLine(commandlineArguments);
                }
            });
        }
    }
}
