using DDNSUpdate.Application.Providers.GoDaddy.Domain;
using DDNSUpdate.Domain;
using DDNSUpdate.Infrastructure.Extensions;
using FluentResults;
using System.Threading;
using System.Threading.Tasks;
using DDNSUpdate.Application.Mutations;

namespace DDNSUpdate.Application.Providers.GoDaddy
{
    public class GoDaddyDomainProcessor : IGoDaddyDomainProcessor
    {
        private readonly IGoDaddyDNSRecordCreator _dnsRecordCreator;
        private readonly IDNSRecordCollectionMutator _dnsRecordMutator;
        private readonly IGoDaddyDNSRecordReader _dnsRecordReader;
        private readonly IGoDaddyDNSRecordUpdater _dnsRecordUpdater;

        public GoDaddyDomainProcessor(IGoDaddyDNSRecordCreator dnsRecordCreator, IDNSRecordCollectionMutator dnsRecordMutator, IGoDaddyDNSRecordReader dnsRecordReader, IGoDaddyDNSRecordUpdater dnsRecordUpdater)
        {
            _dnsRecordCreator = dnsRecordCreator;
            _dnsRecordMutator = dnsRecordMutator;
            _dnsRecordReader = dnsRecordReader;
            _dnsRecordUpdater = dnsRecordUpdater;
        }

        public async Task<Result> ProcessAsync(GoDaddyDomain domain, ExternalAddress externalAddress, GoDaddyAuthenticationDetails authentication, CancellationToken cancellation)
        {
            Result<DNSRecordCollection> activeDnsRecordsResult = await _dnsRecordReader.ReadAsync(domain.Name, authentication, cancellation);
            if (activeDnsRecordsResult.IsFailed)
            {
                return activeDnsRecordsResult;
            }

            IDNSRecordCollectionMutation[] mutations = GetMutations(externalAddress);

            DNSRecordCollection configurationRecords = new DNSRecordCollection(domain.Records);
            DNSRecordCollection hydratedDnsRecords = _dnsRecordMutator.Mutate(configurationRecords, mutations);
            DNSRecordCollection newRecords = activeDnsRecordsResult.Value.WhereNew(hydratedDnsRecords);
            DNSRecordCollection updatedRecords = activeDnsRecordsResult.Value.WhereUpdated(hydratedDnsRecords);

            Result create = await _dnsRecordCreator.CreateAsync(domain.Name, newRecords, authentication, cancellation);
            Result update = await _dnsRecordUpdater.UpdateAsync(domain.Name, updatedRecords, authentication, cancellation);
            return activeDnsRecordsResult.Merge(create, update);
        }

        private IDNSRecordCollectionMutation[] GetMutations(ExternalAddress externalAddress)
        {
            return new IDNSRecordCollectionMutation[]
            {
                new WithRecordType(DNSRecordType.A),
                new WithUpdatedData(externalAddress.ToIPv4String()!)
            };
        }
    }
}
