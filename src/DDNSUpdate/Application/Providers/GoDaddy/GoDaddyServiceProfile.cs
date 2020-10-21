using DDNSUpdate.Application.Providers.GoDaddy.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyServiceProfile : HostServiceProfile
    {
        private const string _goDaddyConfigurationSection = "GoDaddy";

        public override void Configure(IHostServiceProfileContext context)
        {
            IConfigurationSection configurationSection = context.Configuration.GetSection(_goDaddyConfigurationSection);
            
            context.Services.AddHttpClient<IGoDaddyClient, GoDaddyClient>();

            context.Services.Configure<GoDaddyConfiguration>(configurationSection);

            context.Services.AddTransient<IDDNSService, GoDaddyDDNSService>();

            context.Services.AddTransient<IGoDaddyAccountProcessor, GoDaddyAccountProcessor>();
            context.Services.AddTransient<IGoDaddyDomainProcessor, GoDaddyDomainProcessor>();
            context.Services.AddTransient<IGoDaddyDNSRecordCreator, GoDaddyDNSRecordCreator>();
            context.Services.AddTransient<IGoDaddyDNSRecordReader, GoDadyDNSRecordReader>();
            context.Services.AddTransient<IGoDaddyDNSRecordUpdater, GoDaddyDNSRecordUpdater>();
        }
    }
}
