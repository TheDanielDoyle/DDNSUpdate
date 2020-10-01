using DDNSUpdate.Infrastructure.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace DDNSUpdate
{
    internal class Program
    {
        private static IHost BuildHost(string[] commandlineArguments)
        {
            IConfigurationConfigurator configurationConfigurator = new ConfigurationConfigurator();
            ILoggingConfigurator loggingConfigurator = new LoggingConfigurator();
            IApplicationHostBuilder hostBuilder = new ApplicationHostBuilder(configurationConfigurator, loggingConfigurator);
            return hostBuilder.Build(commandlineArguments);
        }

        private static async Task<int> Main(string[] commandlineArguments)
        {
            IHost host = BuildHost(commandlineArguments);
            try
            {
                Log.Information("DDNS Update {Version} starting", AssemblyHelper.ProductVersion);
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "DDNS Update terminated unexpectedly.");
                return ReturnCode.Fail;
            }
            finally
            {
                host.Dispose();
                Log.CloseAndFlush();
            }
            Log.Information("DDNS Update stopping.");
            return ReturnCode.OK;
        }
    }
}
