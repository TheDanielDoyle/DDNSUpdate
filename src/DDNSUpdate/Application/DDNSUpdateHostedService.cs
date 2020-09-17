using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

namespace DDNSUpdate.Application
{
    public class DDNSUpdateHostedService : BackgroundService
    {
        private readonly IOptionsMonitor<ApplicationConfiguration> _configuration;
        private readonly IEnumerable<IDDNSUpdateService> _updateServices;

        public DDNSUpdateHostedService(
            IOptionsMonitor<ApplicationConfiguration> configuration,
            IEnumerable<IDDNSUpdateService> updateServices
        )
        {
            _configuration = configuration;
            _updateServices = updateServices;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                try
                {
                    await UpdateDDNSMultiple();
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


        private async Task UpdateDDNSMultiple()
        {
            foreach(IDDNSUpdateService service in _updateServices)
            {
                await service.Update();
            }
        }

        private async Task UpdateDDNS(IDDNSUpdateService service)
        {
            try
            {
                await service.Update();
            }
            catch
            {
                Log.Logger.Error("Something went wrong");
            }
        }
    }
}
