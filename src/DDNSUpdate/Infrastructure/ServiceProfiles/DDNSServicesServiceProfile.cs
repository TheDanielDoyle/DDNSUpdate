using DDNSUpdate.Application;
using DDNSUpdate.Application.Providers.DigitalOcean;
using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Infrastructure.ServiceProfiles
{
    public class DDNSServicesServiceProfile : HostServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddSingleton<IScopeBuilder, ScopeBuilder>();
            context.Services.AddSingleton<IDDNSUpdateInvoker, DDNSUpdateInvoker>();

            context.Services.AddScoped<IDDNSService, DigitalOceanDDNSService>();
        }
    }
}
