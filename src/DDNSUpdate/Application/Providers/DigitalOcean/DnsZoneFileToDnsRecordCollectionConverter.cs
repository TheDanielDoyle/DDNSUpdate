using DDNSUpdate.Domain;
using DnsZone;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DnsZoneFileToDnsRecordCollectionConverter : IDnsZoneFileToDNSRecordCollectionConverter
    {
        public DNSRecordCollection Convert(DnsZoneFile dnsZoneFile)
        {
            throw new System.NotImplementedException();
        }
    }
}