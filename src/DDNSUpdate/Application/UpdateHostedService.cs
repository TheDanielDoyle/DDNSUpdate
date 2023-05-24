using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Infrastructure.Settings;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DDNSUpdate.Application;

internal sealed class UpdateHostedService : BackgroundService
{
    private readonly ILogger<UpdateHostedService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public UpdateHostedService(ILogger<UpdateHostedService> logger, IServiceScopeFactory serviceScopeFactory)
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
                await ValidateAndUpdateAsync(scope, cancellationToken);
            }
            finally
            {
                if (!cancellationToken.IsCancellationRequested)
                {
                    TimeSpan? updateInterval = scope.GetRequiredService<AppSettings>().UpdateInterval;
                    _logger.LogInformation("Waiting {Minutes}", updateInterval!.Value.Humanize());
                    await Task.Delay(updateInterval.Value, cancellationToken);
                }
            }
        }
    }

    private async Task UpdateAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        IEnumerable<IUpdateService> updateServices = scope.GetRequiredService<IEnumerable<IUpdateService>>();
        bool cancel = false;
        foreach (IUpdateService updateService in updateServices)
        {
            if (cancel)
            {
                return;
            }
            UpdateResult update = await updateService.UpdateAsync(cancellationToken);
            update.Switch(
                success =>
                {
                    _logger.LogInformation("{Message}", success.Message);
                },
                failed =>
                {
                    _logger.LogUpdateFailed(failed);
                },
                cancelled =>
                {
                    _logger.LogInformation("{Message}", cancelled.Message);
                    cancel = true;
                });
        }
    }
    
    private async Task ValidateAndUpdateAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        await scope
            .GetRequiredService<ISettingsValidator>()
            .Validate()
            .Match(
                async _ =>
                {
                    _logger.LogInformation("Settings validation successful");
                    _logger.LogInformation("Updating DNS entries");
                    await UpdateAsync(scope, cancellationToken);
                },
                invalid =>
                {
                    _logger.LogError("Settings validation failed");
                    _logger.LogValidationErrors(invalid.Results);
                    return Task.CompletedTask;
                }
            );
    }
}