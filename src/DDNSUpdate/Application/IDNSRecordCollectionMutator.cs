using DDNSUpdate.Domain;

namespace DDNSUpdate.Application
{
    public interface IDNSRecordCollectionMutator
    {
        DNSRecordCollection Mutate(DNSRecordCollection dnsRecords, params IDNSRecordCollectionMutation[] mutators);
    }
}