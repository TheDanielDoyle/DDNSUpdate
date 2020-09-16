using DDNSUpdate.Application;
using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Infrastructure.ServiceProfiles
{
    public class HostedServiceProfile : HostServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddHostedService<DDNSUpdateHostedService>();
        }
    }
}
