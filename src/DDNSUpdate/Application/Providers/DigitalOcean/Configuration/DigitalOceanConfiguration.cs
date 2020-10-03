using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean.Configuration
{
    public class DigitalOceanConfiguration
    {
        public IList<DigitalOceanAccount> Accounts { get; set; } = new List<DigitalOceanAccount>();
    }
}
