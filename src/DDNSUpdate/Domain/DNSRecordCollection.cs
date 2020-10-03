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
            return GetNotMatchingRecords(this, compareTo, DNSRecordNameTypeEqualityComparer.Instance);
        }

        public DNSRecordCollection WhereUpdated(DNSRecordCollection compareTo)
        {
            DNSRecordCollection commonRecords = GetMatchingRecords(this, compareTo, DNSRecordNameTypeEqualityComparer.Instance);
            if (commonRecords.Any())
            {
                return GetNotMatchingRecords(commonRecords, compareTo, DNSRecordEqualityComparer.Instance);
            }
            return Empty();
        }

        public DNSRecordCollection WithRecordType(DNSRecordType dnsRecordType)
        {
            return new DNSRecordCollection(this.Where(r => r.Type == dnsRecordType));
        }
        public DNSRecordCollection WithUpdatedDataFrom(string data)
        {
            return ApplyToRecord(r => r.Data = data);
        }

        public DNSRecordCollection WithUpdatedIdsFrom(DNSRecordCollection dnsRecords)
        {
            return ApplyToRecord(d => d.Id = FindId(d, dnsRecords));
        }

        private static DNSRecordCollection Clone(DNSRecordCollection from)
        {
            return new DNSRecordCollection(from.Select(Map));
        }

        private static string? FindId(DNSRecord record, DNSRecordCollection dnsRecords)
        {
            return dnsRecords.FirstOrDefault(r => r.Type == record.Type && r.Name == record.Name)?.Id;
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
                Id = from.Id,
                Name = from.Name,
                Port = from.Port,
                Priority = from.Priority,
                Tag = from.Tag,
                TTL = from.TTL,
                Type = DNSRecordType.FromValue(from.Type.Value),
                Weight = from.Weight
            };
        }

        private DNSRecordCollection ApplyToRecord(Action<DNSRecord> action)
        {
            DNSRecordCollection clone = Clone(this);
            foreach (DNSRecord dnsRecord in clone)
            {
                action(dnsRecord);
            }
            return clone;
        }
    }
}
