using System;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DDNSUpdate.Application
{
    public class DDNSUpdateHostedService : BackgroundService
    {
        private readonly IOptionsMonitor<ApplicationConfiguration> _configuration;

        public DDNSUpdateHostedService(IOptionsMonitor<ApplicationConfiguration> configuration)
        {
            _configuration = configuration;
        }
        
        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                }
                catch (TaskCanceledException)
                {
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
