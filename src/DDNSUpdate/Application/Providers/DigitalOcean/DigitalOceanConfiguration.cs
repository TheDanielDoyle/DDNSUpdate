using System.Collections.Generic;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanConfiguration
    {
        public IEnumerable<DigitalOceanAccount> Accounts { get; set; } = new List<DigitalOceanAccount>();
    }
}
