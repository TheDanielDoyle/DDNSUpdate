using System;
using System.Collections.Generic;
using System.Linq;
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
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unhandled error in {ValidateAndUpdate}", nameof(ValidateAndUpdateAsync));
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

                    IEnumerable<IUpdateService> updateServices =
                        scope.GetRequiredService<IEnumerable<IUpdateService>>();
                    await Parallel.ForEachAsync(updateServices.ToList(), cancellationToken, async (s, c) =>
                    {
                        await s.UpdateAsync(c);
                    });
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