using System.Collections.Generic;
using DDNSUpdate.Application.Providers.DigitalOcean.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanConfiguration
    {
        public IEnumerable<DigitalOceanAccount> Accounts { get; set; } = new List<DigitalOceanAccount>();
    }
}
