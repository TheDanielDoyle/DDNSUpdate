using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DDNSUpdate.Domain
{
    public class DNSRecordCollection : ReadOnlyCollection<DNSRecord>
    {
        public DNSRecordCollection(params DNSRecord[] dnsRecords) : base(dnsRecords.Select(r => r).ToList())
        {
        }

        public DNSRecordCollection(IList<DNSRecord> dnsRecords) : base(dnsRecords)
        {
        }

        public DNSRecordCollection(params IEnumerable<DNSRecord>[] dnsRecords) : base(dnsRecords.SelectMany(r => r).ToList())
        {
        }

        public static DNSRecordCollection Empty()
        {
            return new DNSRecordCollection(new DNSRecord[] { });
        }

        public DNSRecordCollection WhereNew(DNSRecordCollection compareTo)
        {
            return this.IntersectWith(compareTo, DNSRecordNameTypeEqualityComparer.Instance);
        }

        public DNSRecordCollection WhereUpdated(DNSRecordCollection compareTo)
        {
            DNSRecordCollection newRecords = this.WhereNew(compareTo);
            DNSRecordCollection updatedRecords = this.IntersectWith(compareTo, DNSRecordEqualityComparer.Instance);
            DNSRecordCollection updatedRecordsWithoutNewRecords = new(updatedRecords.Except(newRecords, DNSRecordNameTypeEqualityComparer.Instance));
            return updatedRecordsWithoutNewRecords;
        }

        private DNSRecordCollection IntersectWith(DNSRecordCollection compareTo, IEqualityComparer<DNSRecord> equalityComparer)
        {
            DNSRecordCollection records = new(compareTo.Where(r => !this.Contains(r, equalityComparer)));
            return records.Any() ? records : Empty();
        }

        public DNSRecordCollection WithRecordType(DNSRecordType dnsRecordType)
        {
            return new DNSRecordCollection(this.Where(r => r.Type == dnsRecordType));
        }

        public DNSRecordCollection WithUpdatedDataFrom(string data)
        {
            return new DNSRecordCollection(this.Select(d => d with { Data = data }));
        }

        public DNSRecordCollection WithUpdatedIdsFrom(DNSRecordCollection dnsRecords)
        {
            return new DNSRecordCollection(this.Select(d => d with { Id = FindId(d, dnsRecords) }));
        }

        private static string? FindId(DNSRecord record, DNSRecordCollection dnsRecords)
        {
            return dnsRecords.FirstOrDefault(r => r.Type == record.Type && r.Name == record.Name)?.Id;
        }
    }
}
