using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Http.BatchFormatters;
using System;

namespace DDNSUpdate.Infrastructure.Hosting
{
    public class LoggingConfigurator : ILoggingConfigurator
    {
        private const string _enableFileKey = "Logging:EnableFile";
        private const string _fileTemplate = "logs/{0}_log.txt";
        private const string _logstashEndpointKey = "Logging:LogstashEndpoint";
        private const string _seqEndpointKey = "Logging:SeqEndpoint";

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
                    .WriteTo.Console();
            }
            else
            {
                configuration
                    .MinimumLevel.Information()
                    .WriteTo.Console();
            }
            ConfigureFile(context, configuration);
            ConfigureLogstash(context, configuration);
            ConfigureSeq(context, configuration);
        }

        private static void ConfigureFile(HostBuilderContext context, LoggerConfiguration configuration)
        {
            bool enableFile = context.Configuration.GetValue<bool>(_enableFileKey);
            if (enableFile)
            {
                string fileTemplate = string.Format(_fileTemplate, nameof(DDNSUpdate));
                configuration.WriteTo.File(fileTemplate, rollingInterval: RollingInterval.Day);
            }
        }

        private static void ConfigureLogstash(HostBuilderContext context, LoggerConfiguration configuration)
        {
            Uri logstashEndpoint = context.Configuration.GetValue<Uri>(_logstashEndpointKey);
            if (logstashEndpoint != null)
            {
                configuration.WriteTo.DurableHttpUsingFileSizeRolledBuffers(
                    batchFormatter: new ArrayBatchFormatter(),
                    requestUri: logstashEndpoint.ToString(),
                    textFormatter: new ElasticsearchJsonFormatter());
            }
        }

        private static void ConfigureSeq(HostBuilderContext context, LoggerConfiguration configuration)
        {
            Uri seqEndpoint = context.Configuration.GetValue<Uri>(_seqEndpointKey);
            if (seqEndpoint != null)
            {
                configuration.WriteTo.Seq(seqEndpoint.ToString());
            }
        }
    }
}
