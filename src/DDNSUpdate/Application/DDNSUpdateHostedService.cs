using System;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
                    await _invoker.InvokeAsync(cancellation);
                }
                catch (TaskCanceledException)
                {
                }
                catch(Exception exception)
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
    }
}
