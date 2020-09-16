using DDNSUpdate.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Infrastructure.ServiceProfiles
{
    public class ConfigurationServiceProfile : HostedServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.Configure<ApplicationConfiguration>(context.Configuration);
        }
    }
}
