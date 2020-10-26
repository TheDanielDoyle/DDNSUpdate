using DDNSUpdate.Domain;

namespace DDNSUpdate.Application.Mutations
{
    public class WithUpdatedData : IDNSRecordCollectionMutation
    {
        private readonly string _data;

        public WithUpdatedData(string data)
        {
            _data = data;
        }

        public DNSRecordCollection Mutate(DNSRecordCollection dnsRecords)
        {
            return dnsRecords.WithUpdatedDataFrom(_data);
        }
    }
}