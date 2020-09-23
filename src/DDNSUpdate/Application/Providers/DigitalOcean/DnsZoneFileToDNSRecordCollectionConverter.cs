using DDNSUpdate.Domain;
using DnsZone;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DnsZoneFileToDNSRecordCollectionConverter : IDnsZoneFileToDNSRecordCollectionConverter
    {
        public DNSRecordCollection Convert(DnsZoneFile dnsZoneFile)
        {
            throw new System.NotImplementedException();
        }
    }
}