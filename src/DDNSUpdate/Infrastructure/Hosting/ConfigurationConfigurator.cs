using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace DDNSUpdate.Infrastructure.Hosting;

public class ConfigurationConfigurator : IConfigurationConfigurator
{
    private static readonly string _configurationFilename = "config";
    private static readonly string _keyPerFileDirectory = "/run/secrets";

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
            if (!environment.IsProduction())
            {
                builder
                    .AddJsonFile($"{_configurationFilename}.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddYamlFile($"{_configurationFilename}.{environment.EnvironmentName}.yaml", optional: true, reloadOnChange: true)
                    .AddYamlFile($"{_configurationFilename}.{environment.EnvironmentName}.yml", optional: true, reloadOnChange: true);
            }

            builder
                .AddEnvironmentVariables()
                .AddCommandLine(commandlineArguments)
                .AddKeyPerFile(_keyPerFileDirectory, optional: true);
        });
    }
}