using DDNSUpdate.Domain;
using System.Linq;

namespace DDNSUpdate.Application.ExternalAddresses
{
    public class DNSRecordCollectionExternalAddressHydrater : IDNSRecordCollectionExternalAddressHydrater
    {
        public DNSRecordCollection Hydrate(DNSRecordCollection dnsRecords, ExternalAddress externalAddress, DNSRecordType dnsRecordType)
        {
            DNSRecordCollection ipV4Records = dnsRecords.OfRecordType(dnsRecordType);
            return ipV4Records.Any() ? ipV4Records.WithUpdatedData(externalAddress.IPv4Address!.ToString()) : dnsRecords;
        }
    }
}
