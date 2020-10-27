using DDNSUpdate.Application.Providers.DigitalOcean.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Mutations;

namespace DDNSUpdate.Application.Providers.DigitalOcean
{
    public class DigitalOceanDomainProcessor : IDigitalOceanDomainProcessor
    {
        private readonly IDigitalOceanDNSRecordCreator _dnsRecordCreator;
        private readonly IDNSRecordCollectionMutator _dnsRecordMutator;
        private readonly IDigitalOceanDNSRecordReader _dnsRecordReader;
        private readonly IDigitalOceanDNSRecordUpdater _dnsRecordUpdater;

        public DigitalOceanDomainProcessor(IDNSRecordCollectionMutator dnsRecordMutator, IDigitalOceanDNSRecordCreator dnsRecordCreator, IDigitalOceanDNSRecordReader dnsRecordReader, IDigitalOceanDNSRecordUpdater dnsRecordUpdater)
        {
            _dnsRecordMutator = dnsRecordMutator;
            _dnsRecordCreator = dnsRecordCreator;
            _dnsRecordReader = dnsRecordReader;
            _dnsRecordUpdater = dnsRecordUpdater;
        }

        public async Task<Result> ProcessAsync(DigitalOceanDomain domain, ExternalAddress externalAddress, string token, CancellationToken cancellation)
        {
            Result<DNSRecordCollection> activeDnsRecordsResult = await _dnsRecordReader.ReadAsync(domain, token, cancellation);
            if (activeDnsRecordsResult.IsFailed)
            {
                return activeDnsRecordsResult;
            }

            IDNSRecordCollectionMutation[] mutations = GetMutations(externalAddress, activeDnsRecordsResult.Value);

            DNSRecordCollection configurationRecords = new DNSRecordCollection(domain.Records);
            DNSRecordCollection hydratedDnsRecords = _dnsRecordMutator.Mutate(configurationRecords, mutations);
            DNSRecordCollection newRecords = activeDnsRecordsResult.Value.WhereNew(hydratedDnsRecords);
            DNSRecordCollection updatedRecords = activeDnsRecordsResult.Value.WhereUpdated(hydratedDnsRecords);
            
            Result create = await _dnsRecordCreator.CreateAsync(domain.Name, newRecords, token, cancellation);
            Result update = await _dnsRecordUpdater.UpdateAsync(domain.Name, updatedRecords, token, cancellation);
            return activeDnsRecordsResult.Merge(create, update);
        }

        private IDNSRecordCollectionMutation[] GetMutations(ExternalAddress externalAddress, DNSRecordCollection idsFrom)
        {
            return new IDNSRecordCollectionMutation[]
            {
                new WithRecordType(DNSRecordType.A),
                new WithUpdatedData(externalAddress.ToIPv4String()!),
                new WithUpdatedIds(idsFrom)
            };
        }
    }
}
