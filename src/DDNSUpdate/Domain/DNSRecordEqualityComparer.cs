using System;
using System.Collections.Generic;

namespace DDNSUpdate.Domain
{
    public sealed class DNSRecordEqualityComparer : EqualityComparer<DNSRecord>
    {
        public static readonly DNSRecordEqualityComparer Instance = new DNSRecordEqualityComparer();

#pragma warning disable 8765

        public override bool Equals(DNSRecord x, DNSRecord y)
#pragma warning restore 8765
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }
            return x.Data == y.Data && x.Flags == y.Flags && x.Name == y.Name && x.Port == y.Port && x.Priority == y.Priority && x.Tag == y.Tag && x.TTL == y.TTL && x.Type.Equals(y.Type) && x.Weight == y.Weight;
        }

        public override int GetHashCode(DNSRecord obj)
        {
            HashCode hashCode = new HashCode();
            hashCode.Add(obj.Data);
            hashCode.Add(obj.Flags);
            hashCode.Add(obj.Name);
            hashCode.Add(obj.Port);
            hashCode.Add(obj.Priority);
            hashCode.Add(obj.Tag);
            hashCode.Add(obj.TTL);
            hashCode.Add(obj.Type);
            hashCode.Add(obj.Weight);
            return hashCode.ToHashCode();
        }
    }
}
