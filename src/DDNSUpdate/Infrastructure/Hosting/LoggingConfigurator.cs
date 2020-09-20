using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace DDNSUpdate.Infrastructure.Hosting
{
    public class LoggingConfigurator : ILoggingConfigurator
    {
        private static readonly string _detaultSeqUrl = "http://localhost:5341";

        public void Configure(IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog(ConfigureLogging);
        }

        private static void ConfigureLogging(HostBuilderContext context, LoggerConfiguration configuration)
        {
            IHostEnvironment environment = context.HostingEnvironment;
            configuration
                .Enrich.FromLogContext()
                .Enrich.WithAssemblyName()
                .MinimumLevel.Override(nameof(Microsoft), LogEventLevel.Error)
                .MinimumLevel.Override(nameof(System), LogEventLevel.Error);
            if (environment.IsDevelopment())
            {
                configuration
                    .MinimumLevel.Debug()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.Seq(_detaultSeqUrl);
            }
            else
            {
                configuration
                    .MinimumLevel.Information()
                    .WriteTo.Console();
            }
        }
    }
}
