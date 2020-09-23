using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanServiceProfile : HostServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddHttpClient<IDigitalOceanClient, DigitalOceanClient>();

            context.Services.AddScoped<IDDNSService, DigitalOceanDDNSService>();
            context.Services.AddScoped<IDigitalOceanDomainProcessor, DigitalOceanDomainProcessor>();
        }
    }
}
