using ServiceProfiles;
using Microsoft.Extensions.DependencyInjection;
using DDNSUpdate.Application.ExternalAddresses;

namespace DDNSUpdate.Infrastructure.ServiceProfiles
{
    public class ExternalAddressProfile : HostServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddHttpClient<IExternalAddressClient, ExternalAddressClient>();
        }
    }
}
