using System;
using DDNSUpdate.Domain;

namespace DDNSUpdate.Application
{
    public class DNSRecordCollectionHydrater : IDNSRecordCollectionHydrater
    {
        public DNSRecordCollectionHydrater()
        {
        }

        public DNSRecordCollection Hydrate(DNSRecordCollection dnsRecords, DNSRecordCollection dnsRecordsToMerge, ExternalAddress externalAddress, DNSRecordType dnsRecordType, Func<DNSRecordCollection, DNSRecordCollection, DNSRecordCollection> mutateDnsRecords = null!)
        {
            DNSRecordCollection result =  dnsRecords
                .WithRecordType(dnsRecordType)
                .WithUpdatedDataFrom(externalAddress.IPv4Address!.ToString());
            if (mutateDnsRecords != null)
            {
                result = mutateDnsRecords.Invoke(dnsRecords, dnsRecordsToMerge);
            }
            return result;
        }
    }
}