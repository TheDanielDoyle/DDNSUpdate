using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.GoDaddy.Domain
{
    public record GoDaddyAccount
    {
        public string ApiKey { get; set; } = default!;

        public string ApiSecret { get; set; } = default!;

        public IList<GoDaddyDomain> Domains { get; set; } = new List<GoDaddyDomain>();
    }
}
