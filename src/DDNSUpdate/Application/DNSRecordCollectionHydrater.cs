using DDNSUpdate.Domain;

namespace DDNSUpdate.Application
{
    public class DNSRecordCollectionHydrater : IDNSRecordCollectionHydrater
    {
        public DNSRecordCollectionHydrater()
        {
        }

        public DNSRecordCollection Hydrate(DNSRecordCollection dnsRecords, DNSRecordCollection dnsRecordsToMerge, ExternalAddress externalAddress, DNSRecordType dnsRecordType)
        {
            return dnsRecords
                .WithRecordType(dnsRecordType)
                .WithUpdatedDataFrom(externalAddress.IPv4Address!.ToString())
                .WithUpdatedIdsFrom(dnsRecordsToMerge);
        }
    }
}