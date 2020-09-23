using DDNSUpdate.Domain;
using DnsZone;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public interface IDnsZoneFileToDNSRecordCollectionConverter
    {
        DNSRecordCollection Convert(DnsZoneFile dnsZoneFile);
    }
}
