using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Results;
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
                await ValidateAndUpdateProvidersAsync(scope, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unhandled error in {ValidateAndUpdate}", nameof(ValidateAndUpdateProvidersAsync));
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
    
    private async Task ValidateAndUpdateProvidersAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        await scope
            .GetRequiredService<ISettingsValidator>()
            .Validate()
            .Match(
                async _ =>
                {
                    _logger.LogInformation("Settings validation successful");
                    _logger.LogInformation("Updating DNS entries");
                    await UpdateProvidersAsync(scope, cancellationToken);
                },
                invalid =>
                {
                    _logger.LogError("Settings validation failed");
                    _logger.LogValidationErrors(invalid.Results);
                    return Task.CompletedTask;
                }
            );
    }

    //TODO: Need a dynamic solution to these update methods. Maybe a provider classes with abstract providing the meat
    private async Task UpdateProvidersAsync(IServiceScope scope, CancellationToken cancellationToken)
    {
        AppSettings appSettings = scope
            .GetRequiredService<AppSettings>();

        if (appSettings.HasGoDaddyAccounts())
        {
            await UpdateAccountsAsync(scope, appSettings.GoDaddy!.Accounts!, cancellationToken);
        }

        if (appSettings.HasDigitalOceanAccounts())
        {
            await UpdateAccountsAsync(scope, appSettings.DigitalOcean!.Accounts!, cancellationToken);
        }
    }

    private async Task UpdateAccountsAsync<TAccount>(
        IServiceScope scope,
        IList<TAccount> accounts,
        CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(accounts, cancellationToken, async (account, token) =>
        {
            await UpdateAccountAsync(account, scope, cancellationToken);
        });
    }

    private async Task UpdateAccountAsync<TAccount>(
        TAccount account, 
        IServiceScope scope, 
        CancellationToken cancellationToken)
    {
        IUpdateService<TAccount> service = scope.GetRequiredService<IUpdateService<TAccount>>();
        UpdateResult update = await service.UpdateAsync(account, cancellationToken);
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
            });
    }
}