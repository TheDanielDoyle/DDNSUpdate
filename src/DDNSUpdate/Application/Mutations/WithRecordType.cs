using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Mutations
{
    public class WithRecordType : IDNSRecordCollectionMutation
    {
        private readonly DNSRecordType _dnsRecordType;

        public WithRecordType(DNSRecordType dnsRecordType)
        {
            _dnsRecordType = dnsRecordType;
        }

        public DNSRecordCollection Mutate(DNSRecordCollection dnsRecords)
        {
            return dnsRecords.WithRecordType(_dnsRecordType);
        }
    }
}