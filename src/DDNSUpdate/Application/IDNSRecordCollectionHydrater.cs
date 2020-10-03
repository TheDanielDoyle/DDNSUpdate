using DDNSUpdate.Domain;

namespace DDNSUpdate.Application
{
    public interface IDNSRecordCollectionHydrater
    {
        DNSRecordCollection Hydrate(DNSRecordCollection dnsRecords, DNSRecordCollection dnsRecordsToMerge, ExternalAddress externalAddress, DNSRecordType dnsRecordType);
    }
}