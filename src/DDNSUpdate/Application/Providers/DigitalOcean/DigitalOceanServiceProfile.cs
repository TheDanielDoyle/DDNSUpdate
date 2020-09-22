using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanServiceProfile : HostServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddScoped<IDDNSService, DigitalOceanDDNSService>();
        }
    }
}
