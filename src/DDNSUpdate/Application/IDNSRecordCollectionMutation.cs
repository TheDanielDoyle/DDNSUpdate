using DDNSUpdate.Domain;

namespace DDNSUpdate.Application
{
    public interface IDNSRecordCollectionMutation
    {
        DNSRecordCollection Mutate(DNSRecordCollection dnsRecords);
    }
}