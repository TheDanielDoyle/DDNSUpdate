using System;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DDNSUpdate
{
    internal class Program
    {
        private static async Task<int> Main(string[] commandlineArguments)
        {
            IConfigurationConfigurator configurationConfigurator = new ConfigurationConfigurator();
            ILoggingConfigurator loggingConfigurator = new LoggingConfigurator();
            IApplicationHostBuilder hostBuilder = new ApplicationHostBuilder(configurationConfigurator, loggingConfigurator);

            IHost host = hostBuilder.Build(commandlineArguments);
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
