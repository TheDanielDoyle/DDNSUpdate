using System;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application
{
    public interface IDNSRecordCollectionHydrater
    {
        DNSRecordCollection Hydrate(DNSRecordCollection dnsRecords, DNSRecordCollection dnsRecordsToMerge, ExternalAddress externalAddress, DNSRecordType dnsRecordType, Func<DNSRecordCollection, DNSRecordCollection, DNSRecordCollection> mutateDnsRecords = null!);
    }
}