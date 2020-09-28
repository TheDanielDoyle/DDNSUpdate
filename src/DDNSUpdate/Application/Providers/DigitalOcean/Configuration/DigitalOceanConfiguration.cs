using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Configuration
{
    public class DigitalOceanConfiguration
    {
        public IEnumerable<DigitalOceanAccount> Accounts { get; set; } = new List<DigitalOceanAccount>();
    }
}
