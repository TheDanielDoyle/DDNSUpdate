using DDNSUpdate.Application.Providers.DigitalOcean.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanServiceProfile : HostServiceProfile
    {
        private const string _digitalOceanConfigurationSection = "DigitalOcean";

        public override void Configure(IHostServiceProfileContext context)
        {
            IConfigurationSection configurationSection = context.Configuration.GetSection(_digitalOceanConfigurationSection);

            context.Services.AddHttpClient<IDigitalOceanClient, DigitalOceanClient>();

            context.Services.AddScoped<IDigitalOceanAccountProcessor, DigitalOceanAccountProcessor>();
            context.Services.Configure<DigitalOceanConfiguration>(configurationSection);
            context.Services.AddScoped<IDigitalOceanClient, DigitalOceanClient>();
            context.Services.AddScoped<IDDNSService, DigitalOceanDDNSService>();
            context.Services.AddScoped<IDigitalOceanDNSRecordCreator, DigitalOceanDNSRecordCreator>();
            context.Services.AddScoped<IDigitalOceanDNSRecordReader, DigitalOceanDNSRecordReader>();
            context.Services.AddScoped<IDigitalOceanDNSRecordUpdater, DigitalOceanDNSRecordUpdater>();
            context.Services.AddScoped<IDigitalOceanDomainProcessor, DigitalOceanDomainProcessor>();
        }
    }
}
