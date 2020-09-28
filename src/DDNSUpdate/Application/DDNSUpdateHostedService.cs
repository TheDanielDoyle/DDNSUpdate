using DDNSUpdate.Infrastructure.Configuration;
using FluentResults;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSUpdate.Application
{
    public class DDNSUpdateHostedService : BackgroundService
    {
        private readonly IOptionsMonitor<ApplicationConfiguration> _configuration;
        private readonly IDDNSUpdateInvoker _invoker;
        private readonly ILogger<DDNSUpdateHostedService> _logger;

        public DDNSUpdateHostedService(IOptionsMonitor<ApplicationConfiguration> configuration, IDDNSUpdateInvoker invoker, ILogger<DDNSUpdateHostedService> logger)
        {
            _configuration = configuration;
            _invoker = invoker;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                    Result result = await _invoker.InvokeAsync(cancellation);
                    LogErrors(result);
                }
                catch (TaskCanceledException)
                {
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, exception.Message);
                }
                finally
                {
                    if (!cancellation.IsCancellationRequested)
                    {
                        await Task.Delay(GetUpdateInterval(), cancellation);
                    }
                }
            }
        }

        private TimeSpan GetUpdateInterval()
        {
            return _configuration.CurrentValue.UpdateInterval;
        }

        private void LogErrors(Result result)
        {
            foreach (Error error in result.Errors)
            {
                _logger.LogError(error.Message);
            }
        }
    }
}
