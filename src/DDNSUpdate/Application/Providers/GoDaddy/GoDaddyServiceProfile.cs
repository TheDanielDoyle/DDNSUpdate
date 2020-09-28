using Microsoft.Extensions.DependencyInjection;
using ServiceProfiles;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyServiceProfile : HostServiceProfile
    {
        public override void Configure(IHostServiceProfileContext context)
        {
            context.Services.AddHttpClient<IGoDaddyClient, GoDaddyClient>();
        }
    }
}
