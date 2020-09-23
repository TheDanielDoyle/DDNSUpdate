using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDomain
    {
        public string Name { get; set; } = default!;

        public DNSRecordCollection Records { get; set; } = DNSRecordCollection.Empty;
    }
}