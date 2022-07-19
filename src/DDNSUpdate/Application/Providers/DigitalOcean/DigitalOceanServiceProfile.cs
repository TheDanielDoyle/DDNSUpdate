using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Application.Providers.DigitalOcean;

public class DigitalOceanServiceProfile : HostServiceProfile
{
    private const string _digitalOceanConfigurationSection = "DigitalOcean";

    public override void Configure(IHostServiceProfileContext context)
    {
        IConfigurationSection configurationSection = context.Configuration.GetSection(_digitalOceanConfigurationSection);

        context.Services.AddHttpClient<IDigitalOceanClient, DigitalOceanClient>();

        context.Services.Configure<DigitalOceanConfiguration>(configurationSection);

        context.Services.AddTransient<IDigitalOceanAccountProcessor, DigitalOceanAccountProcessor>();
        context.Services.AddTransient<IDigitalOceanDomainProcessor, DigitalOceanDomainProcessor>();
            
        context.Services.AddTransient<IDDNSService, DigitalOceanDDNSService>();
        context.Services.AddTransient<IDigitalOceanDNSRecordCreator, DigitalOceanDNSRecordCreator>();
        context.Services.AddTransient<IDigitalOceanDNSRecordReader, DigitalOceanDNSRecordReader>();
        context.Services.AddTransient<IDigitalOceanDNSRecordUpdater, DigitalOceanDNSRecordUpdater>();
    }
}