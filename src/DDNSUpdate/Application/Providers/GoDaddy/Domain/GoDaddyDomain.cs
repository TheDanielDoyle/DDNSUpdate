using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Providers.GoDaddy.Domain
{
    public class GoDaddyDomain
    {
        public string Name { get; set; } = default!;

        public DNSRecordCollection Records = DNSRecordCollection.Empty;
    }
}