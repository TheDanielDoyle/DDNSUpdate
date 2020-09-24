using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DDNSUpdate.Domain
{
    public class DNSRecordCollection : ReadOnlyCollection<DNSRecord>
    {
        public static DNSRecordCollection Empty;

        static DNSRecordCollection()
        {
            Empty = new DNSRecordCollection(new DNSRecord[]{});
        }
        
        public DNSRecordCollection(params IEnumerable<DNSRecord>[]  dnsRecords) : base(dnsRecords.SelectMany(r => r).ToList())
        {
        }

        public DNSRecordCollection OfRecordType(DNSRecordType dnsRecordType)
        {
            return new DNSRecordCollection(this.Where(r => r.Type == dnsRecordType));
        }

        public DNSRecordCollection WhereNew(DNSRecordCollection compareTo)
        {
            return GetNotMatchingRecords(this, compareTo, DNSRecordNameTypeEqualityComparer.Instance);
        }

        public DNSRecordCollection WhereUpdated(DNSRecordCollection compareTo)
        {
            DNSRecordCollection commonRecords = GetMatchingRecords(this, compareTo, DNSRecordNameTypeEqualityComparer.Instance);
            return GetNotMatchingRecords(commonRecords, compareTo, DNSRecordEqualityComparer.Instance);
        }

        public DNSRecordCollection WithUpdatedData(string data)
        {
            DNSRecordCollection clone = Clone(this);
            foreach (DNSRecord dnsRecord in clone)
            {
                dnsRecord.Data = data;
            }
            return clone;
        }

        private static DNSRecordCollection Clone(DNSRecordCollection from)
        {
            return new DNSRecordCollection(from.Select(Map));
        }

        private static DNSRecordCollection GetMatchingRecords(DNSRecordCollection compareWith, DNSRecordCollection compareTo, IEqualityComparer<DNSRecord> equalityComparer)
        {
            return new DNSRecordCollection(compareWith.Intersect(compareTo, equalityComparer));
        }

        private static DNSRecordCollection GetNotMatchingRecords(DNSRecordCollection compareWith, DNSRecordCollection compareTo, IEqualityComparer<DNSRecord> equalityComparer)
        {
            return new DNSRecordCollection(compareTo.Where(r => !compareWith.Contains(r, equalityComparer)));
        }

        private static DNSRecord Map(DNSRecord from)
        {
            return new DNSRecord
            {
                Data = from.Data,
                Flags = from.Flags,
                Name = from.Name,
                Port = from.Port,
                Priority = from.Priority,
                Tag = from.Tag,
                TTL = from.TTL,
                Type = DNSRecordType.FromValue(from.Type.Value),
                Weight = from.Weight
            };
        }
    }
}
