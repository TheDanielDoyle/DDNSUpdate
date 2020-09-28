using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Domain
{
    public class DigitalOceanAccount
    {
        public IEnumerable<DigitalOceanDomain> Domains { get; set; } = new List<DigitalOceanDomain>();

        public string Token { get; set; } = default!;
    }
}
