using System;
using System.Threading.Tasks;
using DDNSUpdate.Application;
using DDNSUpdate.Infrastructure;
using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Infrastructure.Profiles;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DDNSUpdate;

internal sealed class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Logger.Information("Starting DDNSUpdate");

            await Host
                .CreateApplicationBuilder(args)
                .AddConfiguration(args)
                .AddHostedService<UpdateHostedService>()
                .AddProfile<ApplicationProfile>()
                .Build()
                .RunAsync();
            return ReturnCode.OK;
        }
        catch (Exception exception)
        {
            Log.Logger.Fatal(exception, "Unhandled error occurred {ErrorMessage}", exception.Message);
            return ReturnCode.Fail;
        }
        finally
        {
            Log.Logger.Information("Stopping DDNSUpdate");
            await Log.CloseAndFlushAsync();
        }
    }
}