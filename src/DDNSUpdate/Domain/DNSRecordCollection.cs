using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DDNSUpdate.Domain
{
    public class DNSRecordCollection : ReadOnlyCollection<DNSRecord>
    {
        public DNSRecordCollection(IEnumerable<DNSRecord> dnsRecords) : base(dnsRecords.ToList())
        {
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

        private static DNSRecordCollection GetMatchingRecords(DNSRecordCollection compareWith, DNSRecordCollection compareTo, IEqualityComparer<DNSRecord> equalityComparer)
        {
            return new DNSRecordCollection(compareWith.Intersect(compareTo, equalityComparer));
        }

        private static DNSRecordCollection GetNotMatchingRecords(DNSRecordCollection compareWith, DNSRecordCollection compareTo, IEqualityComparer<DNSRecord> equalityComparer)
        {
            return new DNSRecordCollection(compareTo.Where(r => !compareWith.Contains(r, equalityComparer)));
        }
    }
}
