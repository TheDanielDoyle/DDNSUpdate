using DDNSUpdate.Domain;

namespace DDNSUpdate.Application
{
    public class DNSRecordCollectionMutator : IDNSRecordCollectionMutator
    {
        public DNSRecordCollection Mutate(DNSRecordCollection dnsRecords, params IDNSRecordCollectionMutation[] mutators)
        {
            DNSRecordCollection hydratedRecords = dnsRecords;
            foreach (IDNSRecordCollectionMutation mutation in mutators)
            {
                hydratedRecords = mutation.Mutate(hydratedRecords);
            }
            return hydratedRecords;
        }
    }
}