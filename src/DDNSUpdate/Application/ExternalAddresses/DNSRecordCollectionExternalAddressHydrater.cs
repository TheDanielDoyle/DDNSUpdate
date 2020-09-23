using System.Linq;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.ExternalAddresses
{
    public class DNSRecordCollectionExternalAddressHydrater : IDNSRecordCollectionExternalAddressHydrater
    {
        public DNSRecordCollection Hydrate(DNSRecordCollection dnsRecords, ExternalAddress externalAddress)
        {
            DNSRecordCollection ipV4Records = dnsRecords.OfRecordType(DNSRecordType.A);
            return ipV4Records.Any() ? ipV4Records.WithUpdatedData(externalAddress.IPv4Address!.ToString()) : DNSRecordCollection.Empty;
        }
    }
}