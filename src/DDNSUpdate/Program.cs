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
                Log.Information("DDNSUpdate starting.");
                await host.RunAsync();
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "DDNSUpdate terminated unexpectedly.");
                return ReturnCode.Fail;
            }
            finally
            {
                host.Dispose();
                Log.CloseAndFlush();
            }
            Log.Information("DDNSUpdate stopping.");
            return ReturnCode.OK;
        }
    }
}
