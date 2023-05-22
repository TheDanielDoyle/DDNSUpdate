using System;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Infrastructure.Settings;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application;

internal sealed class UpdateService : BackgroundService
{
    private readonly ILogger<UpdateService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public UpdateService(ILogger<UpdateService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        
        while (!cancellationToken.IsCancellationRequested)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            try
            {
                await scope.ServiceProvider
                    .GetRequiredService<ISettingsValidator>()
                    .Validate()
                    .Match(
                        async valid =>
                        {
                            _logger.LogInformation("Updating DNS entries");
                            await Task.CompletedTask;
                        },
                        async invalid =>
                        {
                            _logger.LogError("Settings validation failed");
                            _logger.LogValidationErrors(invalid.Results);
                            await Task.CompletedTask;
                        }
                    );
            }
            finally
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    TimeSpan? updateInterval = scope.ServiceProvider
                        .GetRequiredService<AppSettings>().UpdateInterval;
                    _logger.LogInformation("Waiting {Minutes}", updateInterval!.Value.Humanize());
                    await Task.Delay(updateInterval!.Value, cancellationToken);
                }
            }
        }
    }
}