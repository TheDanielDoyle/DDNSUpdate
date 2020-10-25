using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Mutations
{
    public class WithUpdatedIds : IDNSRecordCollectionMutation
    {
        private readonly DNSRecordCollection _idsFrom;

        public WithUpdatedIds(DNSRecordCollection idsFrom)
        {
            _idsFrom = idsFrom;
        }

        public DNSRecordCollection Mutate(DNSRecordCollection dnsRecords)
        {
            return dnsRecords.WithUpdatedIdsFrom(_idsFrom);
        }
    }
}