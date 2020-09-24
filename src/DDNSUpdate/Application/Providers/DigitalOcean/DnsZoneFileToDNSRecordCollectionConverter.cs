using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using DDNSUpdate.Domain;
using DnsZone;
using DnsZone.Records;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DnsZoneFileToDNSRecordCollectionConverter : IDnsZoneFileToDNSRecordCollectionConverter
    {
        public DNSRecordCollection Convert(DnsZoneFile dnsZoneFile)
        {
            DNSRecordCollection aRecords = MapAResourceRecords(GetRecordsOfType<AResourceRecord>(dnsZoneFile.Records));
            DNSRecordCollection aaaaRecords = MapAAAAResourceRecords(GetRecordsOfType<AaaaResourceRecord>(dnsZoneFile.Records));
            return new DNSRecordCollection(aRecords, aaaaRecords);
        }

        private IEnumerable<T> GetRecordsOfType<T>(IEnumerable<ResourceRecord> records) where T : ResourceRecord
        {
            return records.Where(x => x.GetType().IsAssignableTo<T>()).Cast<T>();
        }

        private DNSRecordCollection MapAAAAResourceRecords(IEnumerable<AaaaResourceRecord> aaaaResourceRecords)
        {
            IEnumerable<DNSRecord> records = aaaaResourceRecords.Select(r => new DNSRecord
            {
                Data = r.Address.ToString(),
                Name = r.Name,
                TTL = r.Ttl.Seconds,
                Type = DNSRecordType.FromValue(r.Type.ToString())
            });
            return new DNSRecordCollection(records);
        }

        private DNSRecordCollection MapAResourceRecords(IEnumerable<AResourceRecord> aResourceRecords)
        {
            IEnumerable<DNSRecord> records = aResourceRecords.Select(r => new DNSRecord
            {
                Data = r.Address.ToString(),
                Name = r.Name,
                TTL = r.Ttl.Seconds,
                Type = DNSRecordType.FromValue(r.Type.ToString())
            });
            return new DNSRecordCollection(records);
        }
    }
}