using System;
using System.Collections.Generic;

namespace DDNSUpdate.Domain
{
    public sealed class DNSRecordNameTypeEqualityComparer : EqualityComparer<DNSRecord>
    {
        public static readonly DNSRecordNameTypeEqualityComparer Instance = new DNSRecordNameTypeEqualityComparer();

        private DNSRecordNameTypeEqualityComparer()
        {
        }

        public override bool Equals(DNSRecord x, DNSRecord y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (x.GetType() != y.GetType())
            {
                return false;
            }
            return x.Name == y.Name && x.Type.Equals(y.Type);
        }

        public override int GetHashCode(DNSRecord dnsRecord)
        {
            return HashCode.Combine(dnsRecord.Name, dnsRecord.Type);
        }
    }
}