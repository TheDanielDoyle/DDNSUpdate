using DDNSUpdate.Infrastructure.Configuration;
using FluentResults;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure;

namespace DDNSUpdate.Application
{
    public class DDNSUpdateHostedService : BackgroundService
    {
        private readonly IOptionsMonitor<ApplicationConfiguration> _configuration;
        private readonly IDDNSUpdateInvoker _invoker;
        private readonly IResultsLogger _logger;

        public DDNSUpdateHostedService(IOptionsMonitor<ApplicationConfiguration> configuration, IDDNSUpdateInvoker invoker, IResultsLogger logger)
        {
            _configuration = configuration;
            _invoker = invoker;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                Result result = Result.Ok();
                try
                {
                    result = await _invoker.InvokeAsync(cancellation);
                }
                catch (TaskCanceledException)
                {
                }
                catch (Exception exception)
                {
                    result = Result.Fail(exception.Message);
                }
                finally
                {
                    _logger.Log(result);
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
