using System;
using DDNSUpdate.Infrastructure.Profiles;
using DDNSUpdate.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DDNSUpdate.Infrastructure.Extensions;

internal static class HostApplicationBuilderExtensions
{
    private const string _configName = "config";
    
    public static HostApplicationBuilder AddConfiguration(this HostApplicationBuilder builder, string[] args)
    {
        builder.Configuration
            .AddJsonFile($"{_configName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{_configName}.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddYamlFile($"{_configName}.yml", optional: true, reloadOnChange: true)
            .AddYamlFile($"{_configName}.{builder.Environment.EnvironmentName}.yml", optional: true, reloadOnChange: true)
            .AddYamlFile($"{_configName}.yaml", optional: true, reloadOnChange: true)
            .AddYamlFile($"{_configName}.{builder.Environment.EnvironmentName}.yaml", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddKeyPerFile("/run/secrets", optional: true)
            .AddCommandLine(args);
        return builder;
    }
    
    public static HostApplicationBuilder AddProfile<TProfile>(this HostApplicationBuilder builder)
        where TProfile : IHostBuilderProfile
    {
        return Activator
            .CreateInstance<TProfile>()
            .Add(builder);
    }

    public static HostApplicationBuilder AddHostedService<TService>(this HostApplicationBuilder builder)
        where TService : class, IHostedService
    {
        builder.Services.AddHostedService<TService>();
        return builder;
    }

    public static HostApplicationBuilder AddSettings<TSettings>(
        this HostApplicationBuilder builder, string? configurationSection = default) 
        where TSettings : class, ISettings
    {
        builder.Services.Configure<TSettings>(
            string.IsNullOrWhiteSpace(configurationSection)
            ? builder.Configuration
            : builder.Configuration.GetSection(configurationSection));

        builder.Services.AddTransient<TSettings>(s => s
            .GetRequiredService<IOptionsSnapshot<TSettings>>().Value);
        
        builder.Services.AddTransient<ISettings, TSettings>(s => s
            .GetRequiredService<TSettings>());
        return builder;
    }
}