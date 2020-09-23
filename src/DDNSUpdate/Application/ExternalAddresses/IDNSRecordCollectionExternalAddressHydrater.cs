using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.ExternalAddresses
{
    public interface IDNSRecordCollectionExternalAddressHydrater
    {
        DNSRecordCollection Hydrate(DNSRecordCollection dnsRecords, ExternalAddress externalAddress);
    }
}
