using DDNSUpdate.Application;
using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Infrastructure.ServiceProfiles
{
    public class DDNSServicesServiceProfile : HostServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddTransient<IScopeBuilder, ScopeBuilder>();
            context.Services.AddSingleton<IDDNSUpdateInvoker, DDNSUpdateInvoker>();
        }
    }
}